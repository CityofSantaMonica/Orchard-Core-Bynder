function getHash(url) {
    const hashIdPrefix = '/m/';
    let hashId = null;

    if (url) {
        const startIndex = url.indexOf(hashIdPrefix) + hashIdPrefix.length;
        let endIndex = -1;

        if (startIndex >= 0) {
            endIndex = url.indexOf('/', startIndex);
        }

        if (startIndex >= 0 && endIndex >= 0) {
            hashId = url.slice(startIndex, endIndex);
        }
    }

    return hashId;
}

function buildImage(bynderImage) {
    const derivatives = ['Desktop Header', 'Inset Image', 'Mobile Header', 'Standard Image', 'Thumbnail'];
    const selectedImage = {
        id: bynderImage.databaseId,
        hashId: getHash(bynderImage.originalUrl),
        name: bynderImage.name,
        description: bynderImage.description,
    };

    const imageDerivatives = [];
    derivatives.forEach((derivative) => {
        if (bynderImage.files[derivative] !== undefined) {
            const namedDerivative = {
                name: derivative,
                url: bynderImage.files[derivative].url,
            };

            imageDerivatives.push(namedDerivative);
        }
    });

    const metaproperties = [];
    bynderImage.metaproperties.nodes.forEach((metaitem) => {
        const metaproperty = {
            name: metaitem.name,
        };

        const options = [];
        metaitem.options.forEach((option) => {
            const optionProperty = {
                label: option.displayLabel,
                value: option.name,
            };

            options.push(optionProperty);
        });

        metaproperty.options = options;
        metaproperties.push(metaproperty);
    });

    selectedImage.derivatives = imageDerivatives;
    selectedImage.metadata = metaproperties;

    return selectedImage;
}

function displayImages($container, images) {
    const $list = $container.find('.bynder-list');
    $list.empty();

    images.forEach((image) => {
        const imageDerivative = image.derivatives.find(({ name }) => name === 'Thumbnail');

        if (imageDerivative !== undefined) {
            const $listItem = $(
                `<li class="bynder-item card">
                    <div class="thumb-container">
                        <img src="${imageDerivative.url}"
                            alt="${image.description}"
                            title="${image.name}">
                    </div>
                    <div class="card-body">
                        <a href="#"
                            class="bynder-item-remove btn btn-light btn-sm float-right"
                            title="${$list.data('remove-text')}">
                            <i class="fa fa-trash" aria-hidden="true"></i>
                        </a>
                        <div class="card-text small">${image.name}</div>
                    </div>
                </li>`);

            $listItem.data('resource', image);

            $listItem.find('.bynder-item-remove').on('click', (e) => {
                e.preventDefault();
                $(e.target).closest('.bynder-item').remove();
            });

            $list.append($listItem);
        }
    });
}

function onMultiSelectSuccess($container, assets) {
    const selectedImages = [];

    assets.forEach((element) => selectedImages.push(buildImage(element)));

    displayImages($container, selectedImages);
}

function getSelectedImages($container) {
    const images = [];

    $container
        .find('.bynder-list li')
        .each((index, item) => images.push($(item).data('resource')));

    return images;
}

jQuery(($) => {
    $('.bynder-trigger').on('click', (e) => {
        e.preventDefault();

        const $parent = $(e.target);
        const $container = $parent.closest('.bynder-container');

        // BynderCompactView is defined just not in this script but in its dependency.
        /* eslint-disable no-undef */

        BynderCompactView.open({
            language: $parent.data('culture'),
            theme: {
                colorButtonPrimary: '#3380FF',
            },
            mode: 'MultiSelect',
            portal: {
                url: $parent.data('portal-url'),
                editable: false,
            },
            selectedAssets: $.map(getSelectedImages($container), (image) => image.id),
            onSuccess: (assets) => onMultiSelectSuccess($container, assets),
        });

        /* eslint-enable no-undef */
    });

    $('.bynder-container').each(
        (index, container) => {
            const $container = $(container);
            const resourecesJson = $container.find('.bynder-resources').val();

            if (resourecesJson !== undefined) {
                displayImages($container, JSON.parse(resourecesJson));
            }

            $container
                .closest('form')
                .submit(() => $container.find('.bynder-resources').val(JSON.stringify(getSelectedImages($container))));
        });

    $('.bynder-list').sortable();
});

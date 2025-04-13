document.addEventListener('DOMContentLoaded', function () {
    console.log('Products script loaded');

    const dropdownButton = document.querySelector('.dropdown-button');
    const dropdownContent = document.querySelector('.dropdown-content');
    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    const productCards = document.querySelectorAll('.product-card');

    if (dropdownButton && dropdownContent) {
        dropdownButton.addEventListener('click', function (e) {
            e.stopPropagation();
            dropdownContent.classList.toggle('show');
        });

        window.addEventListener('click', function () {
            if (dropdownContent.classList.contains('show')) {
                dropdownContent.classList.remove('show');
            }
        });
    }

    if (addToCartButtons) {
        addToCartButtons.forEach(button => {
            button.addEventListener('click', handleAddToCart);
        });
    }

    if (productCards) {
        productCards.forEach(card => {
            card.addEventListener('click', function (e) {
                if (!e.target.closest('.add-to-cart')) {
                    const productId = this.dataset.productId;
                    window.location.href = `/ProductDetails/Index/${productId}`;
                }
            });
        });
    }

    async function handleAddToCart(e) {
        console.log('Add to cart button clicked');
        e.preventDefault();
        e.stopPropagation();

        const button = e.target.closest('.add-to-cart');
        if (!button) {
            console.error('Could not find add-to-cart button');
            return;
        }

        const productCard = button.closest('.product-card');
        if (!productCard) {
            console.error('Could not find product card');
            return;
        }

        const productId = productCard.dataset.productId;
        const productName = productCard.querySelector('h3').textContent;
        const productPrice = parseFloat(productCard.dataset.price);
        const productImage = productCard.querySelector('img').src;

        console.log('Adding product to cart:', { productId, productName, productPrice, productImage });

        const productData = {
            id: parseInt(productId),
            name: productName,
            price: productPrice,
            imageUrl: productImage
        };

        try {
            const added = await addToCart(productData, 1);
            console.log('Add to cart result:', added);
            if (added) {
                const notification = document.createElement('div');
                notification.className = 'cart-notification';
                notification.textContent = `${productName} added to cart!`;
                document.body.appendChild(notification);

                setTimeout(() => {
                    notification.remove();
                }, 2000);
            }
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
    }
});
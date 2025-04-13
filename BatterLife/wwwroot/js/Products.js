document.addEventListener('DOMContentLoaded', function () {
    console.log('Products script loaded'); 

    const categoryFilters = document.querySelectorAll('.category-filters button');
    const dropdownButtons = document.querySelectorAll('.dropdown-content button');
    const dropdownButton = document.querySelector('.dropdown-button');
    const productList = document.querySelector('.product-list');

    let selectedCategory = 'all';
    let sortBy = 'name-asc';

    initEventListeners();

    function initEventListeners() {
        console.log('Initializing event listeners'); 

        categoryFilters.forEach(button => {
            button.addEventListener('click', () => handleCategoryFilter(button));
        });

        dropdownButtons.forEach(button => {
            button.addEventListener('click', () => handleSort(button));
        });

        document.querySelectorAll('.add-to-cart').forEach(button => {
            console.log('Found add-to-cart button:', button); 
            button.addEventListener('click', handleAddToCart);
        });

        document.querySelectorAll('.product-card').forEach(card => {
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


    function filterProducts() {
        productCards.forEach(card => {
            const category = card.dataset.category;
            if (selectedCategory === 'all' || category === selectedCategory) {
                card.style.display = 'block';
            } else {
                card.style.display = 'none';
            }
        });
        sortProducts();
    }

    function sortProducts() {
        const cards = Array.from(document.querySelectorAll('.product-card[style="display: block;"], .product-card:not([style])'));

        cards.sort((a, b) => {
            const nameA = a.dataset.name.toLowerCase();
            const nameB = b.dataset.name.toLowerCase();
            const priceA = parseFloat(a.dataset.price);
            const priceB = parseFloat(b.dataset.price);

            if (sortBy === 'name-asc') return nameA.localeCompare(nameB);
            if (sortBy === 'name-desc') return nameB.localeCompare(nameA);
            if (sortBy === 'price-asc') return priceA - priceB;
            if (sortBy === 'price-desc') return priceB - priceA;
            return 0;
        });

        cards.forEach(card => productList.appendChild(card));
    }

    dropdownButton.addEventListener('click', function (e) {
        e.stopPropagation();
        document.querySelector('.dropdown-content').classList.toggle('show');
    });

    window.addEventListener('click', function () {
        const dropdowns = document.querySelectorAll('.dropdown-content');
        dropdowns.forEach(dropdown => {
            if (dropdown.classList.contains('show')) {
                dropdown.classList.remove('show');
            }
        });
    });
});
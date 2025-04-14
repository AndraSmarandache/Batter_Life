document.addEventListener('DOMContentLoaded', function () {
    const categoryFilters = document.querySelectorAll('.category-filters button');
    const dropdownButtons = document.querySelectorAll('.dropdown-content button');
    const dropdownButton = document.querySelector('.dropdown-button');
    const productCards = document.querySelectorAll('.product-card');
    const productList = document.querySelector('.product-list');

    let selectedCategory = 'all';
    let sortBy = 'name-asc';

    initEventListeners();

    function initEventListeners() {
        categoryFilters.forEach(button => {
            button.addEventListener('click', () => handleCategoryFilter(button));
        });

        dropdownButtons.forEach(button => {
            button.addEventListener('click', () => handleSort(button));
        });

        document.querySelectorAll('.add-to-cart').forEach(button => {
            button.addEventListener('click', handleAddToCart);
        });

        productCards.forEach(card => {
            card.style.cursor = 'pointer';
            card.addEventListener('click', function (e) {
                if (!e.target.closest('.add-to-cart')) {
                    const productId = this.dataset.productId;
                    window.location.href = `/ProductDetails/Index/${productId}`;
                }
            });
        });
    }

    function handleCategoryFilter(button) {
        categoryFilters.forEach(btn => btn.classList.remove('active'));
        button.classList.add('active');
        selectedCategory = button.dataset.category;
        filterProducts();
    }

    function handleSort(button) {
        sortBy = button.dataset.sort;
        dropdownButton.textContent = button.textContent + ' ▼';
        sortProducts();
    }

    function handleAddToCart(e) {
        e.stopPropagation();
        const productCard = e.target.closest('.product-card');
        const productName = productCard.querySelector('h3').textContent;
        alert(`${productName} added to cart!`);
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
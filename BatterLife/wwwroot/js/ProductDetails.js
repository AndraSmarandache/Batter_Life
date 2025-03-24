const products = [
  {
      id: 1,
      name: 'Chocolate Cake',
      category: 'cakes',
      price: 25,
      image: '../images/chocolate-cake.jpg',
      description: 'A rich and decadent chocolate cake, perfect for any occasion.',
      ingredients: ['Chocolate', 'Flour', 'Sugar', 'Eggs', 'Butter', 'Milk'],
      allergens: ['Gluten', 'Eggs', 'Milk'],
      rating: 4.5,
      reviews: [
          { id: 1, user: 'John Doe', comment: 'Amazing cake!', rating: 5 },
          { id: 2, user: 'Jane Smith', comment: 'Very tasty, but a bit too sweet.', rating: 4 },
      ],
  },
  {
      id: 2,
      name: 'Strawberry Tart',
      category: 'pastries',
      price: 15,
      image: '../images/strawberry-tart.jpg',
      description: 'A fresh and fruity strawberry tart with a buttery crust.',
      ingredients: ['Strawberries', 'Cream', 'Pastry', 'Sugar', 'Butter'],
      allergens: ['Gluten', 'Dairy'],
      rating: 4.2,
      reviews: [
          { id: 1, user: 'Alice Johnson', comment: 'Delicious and refreshing!', rating: 5 },
          { id: 2, user: 'Bob Brown', comment: 'The crust could be crispier.', rating: 3 },
      ],
  },
  {
      id: 3,
      name: 'Vanilla Cupcake',
      category: 'cakes',
      price: 5,
      image: '../images/vanilla-cupcake.jpg',
      description: 'A classic vanilla cupcake with creamy frosting.',
      ingredients: ['Vanilla', 'Flour', 'Sugar', 'Eggs', 'Butter', 'Milk'],
      allergens: ['Gluten', 'Eggs', 'Milk'],
      rating: 4.7,
      reviews: [
          { id: 1, user: 'Emily Davis', comment: 'Perfectly sweet and fluffy!', rating: 5 },
          { id: 2, user: 'Michael Wilson', comment: 'The frosting was a bit too sweet.', rating: 4 },
      ],
  },
  {
      id: 4,
      name: 'Almond Cookies',
      category: 'cookies',
      price: 10,
      image: '../images/almond-cookies.jpg',
      description: 'Crunchy almond cookies with a hint of vanilla.',
      ingredients: ['Almonds', 'Flour', 'Sugar', 'Butter', 'Vanilla'],
      allergens: ['Gluten', 'Nuts'],
      rating: 4.0,
      reviews: [
          { id: 1, user: 'Sarah Lee', comment: 'Loved the almond flavor!', rating: 4 },
          { id: 2, user: 'David Clark', comment: 'A bit too dry for my taste.', rating: 3 },
      ],
  },
  {
      id: 5,
      name: 'Cheesecake',
      category: 'cakes',
      price: 20,
      image: '../images/cheesecake.jpg',
      description: 'Creamy cheesecake with a biscuit base and berry topping.',
      ingredients: ['Cream Cheese', 'Biscuits', 'Sugar', 'Berries', 'Butter'],
      allergens: ['Gluten', 'Dairy'],
      rating: 4.8,
      reviews: [
          { id: 1, user: 'Laura Green', comment: 'The best cheesecake I’ve ever had!', rating: 5 },
          { id: 2, user: 'James White', comment: 'A bit too rich, but still delicious.', rating: 4 },
      ],
  },
  {
      id: 6,
      name: 'Macarons',
      category: 'pastries',
      price: 12,
      image: '../images/macarons.jpg',
      description: 'Colorful and delicate French macarons with various flavors.',
      ingredients: ['Almond Flour', 'Sugar', 'Egg Whites', 'Food Coloring'],
      allergens: ['Nuts', 'Eggs'],
      rating: 4.6,
      reviews: [
          { id: 1, user: 'Olivia Harris', comment: 'So pretty and tasty!', rating: 5 },
          { id: 2, user: 'Daniel Martin', comment: 'A bit too sweet for me.', rating: 3 },
      ],
  },
  {
      id: 7,
      name: 'Brownies',
      category: 'cookies',
      price: 8,
      image: '../images/brownies.jpg',
      description: 'Fudgy and chocolatey brownies with a rich flavor.',
      ingredients: ['Chocolate', 'Butter', 'Sugar', 'Flour', 'Eggs'],
      allergens: ['Gluten', 'Eggs'],
      rating: 4.3,
      reviews: [
          { id: 1, user: 'Sophia Martinez', comment: 'Perfectly fudgy and delicious!', rating: 5 },
          { id: 2, user: 'William Anderson', comment: 'Could be more chocolatey.', rating: 4 },
      ],
  },
  {
      id: 8,
      name: 'Tiramisu',
      category: 'desserts',
      price: 18,
      image: '../images/tiramisu.jpg',
      description: 'Classic Italian tiramisu with coffee and mascarpone.',
      ingredients: ['Mascarpone', 'Coffee', 'Ladyfingers', 'Cocoa', 'Sugar'],
      allergens: ['Gluten', 'Dairy'],
      rating: 4.9,
      reviews: [
          { id: 1, user: 'Emma Taylor', comment: 'Absolutely divine!', rating: 5 },
          { id: 2, user: 'Noah Thomas', comment: 'A bit too strong on the coffee flavor.', rating: 4 },
        ],
    }
];

document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);
    const productId = parseInt(urlParams.get('id'));
    const product = products.find(p => p.id === productId);

    if (product) {
        displayProductDetails(product);
    } else {
        document.querySelector('.product-details-content').innerHTML = '<p>Product not found.</p>';
    }
});

function displayProductDetails(product) {
    document.querySelector('.product-details-image').src = product.image;
    document.querySelector('.product-name').textContent = product.name;
    document.querySelector('.price').innerHTML = `<strong>Total Price:</strong> $${product.price}`;
    document.querySelector('.description').textContent = product.description;
    document.querySelector('.ingredients').textContent = `Ingredients: ${product.ingredients.join(', ')}`;
    document.querySelector('.allergens').textContent = `Allergens: ${product.allergens.join(', ')}`;
    document.querySelector('.product-rating').textContent = product.rating;

    const starsContainer = document.querySelector('.stars');
    starsContainer.innerHTML = '★'.repeat(Math.floor(product.rating)) + '☆'.repeat(5 - Math.floor(product.rating));

    let quantity = 1;
    let totalPrice = product.price;

    const quantityInput = document.getElementById('quantity');
    quantityInput.value = quantity;

    document.querySelector('.decrease-quantity').addEventListener('click', () => {
        if (quantity > 1) {
            quantity--;
            quantityInput.value = quantity;
            totalPrice = product.price * quantity;
            document.querySelector('.price').innerHTML = `<strong>Total Price:</strong> $${totalPrice}`;
        }
    });

    document.querySelector('.increase-quantity').addEventListener('click', () => {
        quantity++;
        quantityInput.value = quantity;
        totalPrice = product.price * quantity;
        document.querySelector('.price').innerHTML = `<strong>Total Price:</strong> $${totalPrice}`;
    });

    document.querySelector('.add-to-cart-button').addEventListener('click', () => {
        alert(`Added ${quantity} of ${product.name} to cart. Total: $${totalPrice}`);
    });

    const reviewsContainer = document.querySelector('.reviews');
    const seeReviewsButton = document.querySelector('.see-reviews-button');
    let reviewsVisible = false;

    seeReviewsButton.addEventListener('click', () => {
        reviewsVisible = !reviewsVisible;
        if (reviewsVisible) {
            reviewsContainer.innerHTML = product.reviews.map(review => `
                <div class="review">
                    <strong>${review.user}</strong>
                    <div class="stars">${'★'.repeat(review.rating)}${'☆'.repeat(5 - review.rating)}</div>
                    <p>${review.comment}</p>
                </div>
            `).join('');
            seeReviewsButton.textContent = 'Hide Reviews';
        } else {
            reviewsContainer.innerHTML = '';
            seeReviewsButton.textContent = 'See Reviews';
        }
    });
}
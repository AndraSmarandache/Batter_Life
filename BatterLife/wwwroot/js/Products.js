const categories = [
  { id: 'all', name: 'All' },
  { id: 'cakes', name: 'Cakes' },
  { id: 'pastries', name: 'Pastries' },
  { id: 'cookies', name: 'Cookies' },
  { id: 'desserts', name: 'Desserts' },
];

const products = [
  { id: 1, name: 'Chocolate Cake', category: 'cakes', price: 25, image: '../images/chocolate-cake.jpg', ingredients: ['Chocolate', 'Flour', 'Sugar', 'Eggs'] },
  { id: 2, name: 'Strawberry Tart', category: 'pastries', price: 15, image: '../images/strawberry-tart.jpg', ingredients: ['Strawberries', 'Cream', 'Pastry'] },
  { id: 3, name: 'Vanilla Cupcake', category: 'cakes', price: 5, image: '../images/vanilla-cupcake.jpg', ingredients: ['Vanilla', 'Flour', 'Sugar', 'Eggs'] },
  { id: 4, name: 'Almond Cookies', category: 'cookies', price: 10, image: '../images/almond-cookies.jpg', ingredients: ['Almonds', 'Flour', 'Sugar', 'Butter'] },
  { id: 5, name: 'Cheesecake', category: 'cakes', price: 20, image: '../images/cheesecake.jpg', ingredients: ['Cream Cheese', 'Biscuits', 'Sugar', 'Berries'] },
  { id: 6, name: 'Macarons', category: 'pastries', price: 12, image: '../images/macarons.jpg', ingredients: ['Almond Flour', 'Sugar', 'Egg Whites', 'Food Coloring'] },
  { id: 7, name: 'Brownies', category: 'cookies', price: 8, image: '../images/brownies.jpg', ingredients: ['Chocolate', 'Butter', 'Sugar', 'Flour'] },
  { id: 8, name: 'Tiramisu', category: 'desserts', price: 18, image: '../images/tiramisu.jpg', ingredients: ['Mascarpone', 'Coffee', 'Ladyfingers', 'Cocoa'] },
];

const productList = document.querySelector('.product-list');
const categoryFilters = document.querySelectorAll('.category-filters button');
const dropdownButtons = document.querySelectorAll('.dropdown-content button');
const dropdownButton = document.querySelector('.dropdown-button');

let selectedCategory = 'all';
let sortBy = 'name-asc';

function displayProducts() {
  const filteredProducts = selectedCategory === 'all'
      ? products
      : products.filter(product => product.category === selectedCategory);

  const sortedProducts = [...filteredProducts].sort((a, b) => {
      if (sortBy === 'name-asc') return a.name.localeCompare(b.name);
      if (sortBy === 'name-desc') return b.name.localeCompare(a.name);
      if (sortBy === 'price-asc') return a.price - b.price;
      if (sortBy === 'price-desc') return b.price - a.price;
      return 0;
  });

  productList.innerHTML = '';

  sortedProducts.forEach(product => {
      const productCard = `
          <div class="product-card" data-product-id="${product.id}">
              <div class="product-image">
                  <img src="${product.image}" alt="${product.name}">
                  <div class="overlay">
                      <button class="add-to-cart">Add to Cart</button>
                  </div>
              </div>
              <h3>${product.name}</h3>
              <p>$${product.price}</p>
          </div>
      `;
      productList.innerHTML += productCard;
  });

  const addToCartButtons = document.querySelectorAll('.add-to-cart');
  addToCartButtons.forEach(button => {
      button.addEventListener('click', (event) => {
          event.stopPropagation();
          const productId = button.closest('.product-card').dataset.productId;
          const product = products.find(p => p.id === parseInt(productId));
          handleAddToCart(product);
      });
  });

  const productCards = document.querySelectorAll('.product-card');
  productCards.forEach(card => {
      card.addEventListener('click', () => {
          const productId = card.dataset.productId;
          navigateToProductDetails(productId);
      });
  });
}

function handleAddToCart(product) {
  alert(`${product.name} added to cart!`);
}

function navigateToProductDetails(productId) {
  window.location.href = `ProductDetails.html?id=${productId}`;
}

categoryFilters.forEach(button => {
  button.addEventListener('click', () => {
      categoryFilters.forEach(btn => btn.classList.remove('active'));
      button.classList.add('active');
      selectedCategory = button.dataset.category;
      displayProducts();
  });
});

dropdownButtons.forEach(button => {
  button.addEventListener('click', () => {
      sortBy = button.dataset.sort;
      dropdownButton.textContent = button.textContent + ' ▼';
      displayProducts();
  });
});

displayProducts();

let isLoggedIn = localStorage.getItem("isLoggedIn") === "true";

export function updateNavbar() {
  const loginLink = document.getElementById("login-link");
  const userLink = document.getElementById("user-link");

  if (isLoggedIn) {
      loginLink.style.display = "none";
      userLink.style.display = "inline-block";
  } else {
      loginLink.style.display = "inline-block";
      userLink.style.display = "none";
  }
}

export function logout() {
  isLoggedIn = false;
  localStorage.setItem("isLoggedIn", "false");
  updateNavbar();
  alert("You have been logged out.");
  window.location.href = "../html/login.html";
}

export function closeUserProfile() {
  document.getElementById("user-profile-sidebar").classList.remove("active");
  document.getElementById("user-profile-overlay").style.display = "none";
}

export function showSection(sectionId) {
  const sections = document.querySelectorAll(".profile-section");
  sections.forEach(section => {
      section.classList.remove("active");
      section.style.display = "none";
  });
  const activeSection = document.getElementById(sectionId);
  activeSection.classList.add("active");
  activeSection.style.display = "block";
}

document.addEventListener("DOMContentLoaded", function() {
  document.getElementById("close-profile").addEventListener("click", function () {
      closeUserProfile();
  });

  document.getElementById("user-profile-overlay").addEventListener("click", function () {
      closeUserProfile();
  });
});
import React from "react";
import { Link } from "react-router-dom";
import "../styles/Home.css";

const Home = () => {
  return (
    <div className="home-container">
      <video autoPlay loop muted className="background-video">
        <source src="/videos/background.mp4" type="video/mp4" />
        Your browser does not support the video tag.
      </video>
      <div className="overlay">
        <h1>Welcome to BatterLife</h1>
        <p>Sweet moments, unforgettable taste.</p>
        <Link to="/products" className="explore-button">Explore Our Sweets</Link>
      </div>
    </div>
  );
};

export default Home;

import React from "react";
import "./HeroSection.css"; // Create this CSS file for specific styles
import { Navigate, useNavigate } from "react-router-dom";

const HeroSection = () => {
const navigate= useNavigate();
  return (
    <section className="hero-section py-5">
      <div className="container">
        <div className="row align-items-center">
          <div className="col-lg-6 text-center text-lg-start mb-4 mb-lg-0">
            <h1 className="display-4 fw-bold">
              Discover Your Next{" "}
              <span className="text-purple">Favorite Story</span>
            </h1>
            <p className="lead mt-3 mb-4">
              Dive into a world of books, audiobooks, music, and films - all in
              one place.
            </p>
            <div className="d-flex justify-content-center justify-content-lg-start">
              <button className="btn btn-outline-secondary btn-lg me-3"onClick={()=>navigate("/AboutUsPage")}>
                About Us
              </button>
              <button className="btn btn-outline-secondary btn-lg" onClick={()=>navigate("/Register")}>
                Create Account
              </button>
            </div>
          </div>
          <div className="col-lg-6">
            <div className="row g-3">
              <div className="col-6">
                <div className="hero-image-container">
                <img
                  src="images\HeroImg5.jpg"
                  alt="Book 1"
                  className="img-fluid rounded shadow-sm"
                />
                </div>
              </div>
              <div className="col-6">
                <div className="hero-image-container">
                <img
                  src="\images\HeroImg2.jpg"
                  alt="Book 2"
                  className="img-fluid rounded shadow-sm"
                />
                </div>
              </div>
              <div className="col-6">
                <div className="hero-image-container">
                <img
                  src="\images\HeroImg6.jpg"
                  alt="Book 3"
                  className="img-fluid rounded shadow-sm"
                />
                </div>
              </div>
              <div className="col-6">
                <div className="hero-image-container">
                <img
                  src="\images\HeroImg7.jpg"
                  alt="Book 4"
                  className="img-fluid rounded shadow-sm"
                />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default HeroSection;
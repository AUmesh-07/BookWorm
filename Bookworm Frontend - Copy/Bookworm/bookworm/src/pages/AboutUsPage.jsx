import React from 'react';
import './AboutUsPage.css';
import Footer from '../components/Footer';

const AboutUsPage = () => {
  return (
    <div className="about-us-page container ">
      {/* Hero Section */}
      <div className="row align-items-center mb-5 hero-section">
        <div className="col-lg-6 text-center text-lg-start mb-4 mb-lg-0">
          <h1 className="display-4 fw-bold mb-3">Our Story</h1>
          <p className="lead text-muted">
            Bookworm was born from a simple idea: to make a world of knowledge and creativity accessible to everyone. We started as a small team of book lovers, and today we're a vibrant community dedicated to digital content.
          </p>
        </div>
        <div className="col-lg-6 d-flex justify-content-center my-container">
          <img
            src="\images\OurStory.jpg"
            alt="Our Story"
            className="img-fluid rounded-3 shadow-lg responsive-image"
          />
        </div>
      </div>

      {/* Mission Section */}
      <div className="row align-items-center flex-row-reverse mb-5 mission-section">
        <div className="col-lg-6 text-center text-lg-start mb-4 mb-lg-0">
          <h2 className="fw-bold mb-3">Our Mission</h2>
          <p className="text-muted">
            Our mission is to curate a diverse and high-quality collection of eBooks, audiobooks, and music, empowering our users to discover their next favorite story, wherever they are. We believe in the power of a good narrative to connect people and inspire change.
          </p>
          <ul className="list-unstyled mt-4 mission-list">
            <li className="d-flex align-items-center mb-2">
              <i className="bi bi-check-circle-fill me-2 text-pink"></i>
              <span>High-quality curated content.</span>
            </li>
            <li className="d-flex align-items-center mb-2">
              <i className="bi bi-check-circle-fill me-2 text-pink"></i>
              <span>Seamless cross-device experience.</span>
            </li>
            <li className="d-flex align-items-center mb-2">
              <i className="bi bi-check-circle-fill me-2 text-pink"></i>
              <span>A community built around passion for stories.</span>
            </li>
          </ul>
        </div>
        <div className="col-lg-6 d-flex justify-content-center my-container">
          <img
            src="\images\OurMission.jpg"
            alt="Our Mission"
            className="img-fluid rounded-3 shadow-lg responsive-image"
          />
        </div>
      </div>

      {/* Team Section */}
      {/* <div className="team-section text-center">
        <h2 className="fw-bold mb-5">Meet the Team</h2>
        <div className="row justify-content-center g-4">
          <div className="col-md-4 col-sm-6">
            <div className="card h-100 border-0 shadow-sm team-card">
              <img
                src="https://placehold.co/300x300/FFC107/FFFFFF?text=John+Doe"
                className="card-img-top team-img rounded-circle p-3"
                alt="John Doe"
              />
              <div className="card-body">
                <h5 className="card-title fw-bold">John Doe</h5>
                <p className="card-text text-muted">Co-Founder & CEO</p>
              </div>
            </div>
          </div>
          <div className="col-md-4 col-sm-6">
            <div className="card h-100 border-0 shadow-sm team-card">
              <img
                src="https://placehold.co/300x300/28A745/FFFFFF?text=Jane+Smith"
                className="card-img-top team-img rounded-circle p-3"
                alt="Jane Smith"
              />
              <div className="card-body">
                <h5 className="card-title fw-bold">Jane Smith</h5>
                <p className="card-text text-muted">Chief Technology Officer</p>
              </div>
            </div>
          </div>
          <div className="col-md-4 col-sm-6">
            <div className="card h-100 border-0 shadow-sm team-card">
              <img
                src="https://placehold.co/300x300/17A2B8/FFFFFF?text=Emily+Chen"
                className="card-img-top team-img rounded-circle p-3"
                alt="Emily Chen"
              />
              <div className="card-body">
                <h5 className="card-title fw-bold">Emily Chen</h5>
                <p className="card-text text-muted">Head of Curation</p>
              </div>
            </div>
          </div>
        </div>
      </div> */}
      <Footer/>
    </div>
  );
};

export default AboutUsPage;
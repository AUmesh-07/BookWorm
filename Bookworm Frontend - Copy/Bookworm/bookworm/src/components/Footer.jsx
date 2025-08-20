import React from 'react';
import './Footer.css';
import BookLoader from './BookLoader';
import { Link } from 'react-router-dom';


const Footer = () => {
  return (
    <footer className="footer bg-white py-5 mt-5 border-top">
      <div className="container">
        <div className="row g-4 align-items-start">
          {/* Left Section: Social Media Links */}
          <div className="col-md-4 d-flex flex-column align-items-start">
            <h5 className="fw-bold mb-3 footer-heading">Connect</h5>
            <ul className="list-unstyled social-links">
              <li className="mb-3">
                <a href="https://www.instagram.com/BookwormVirtual" target="_blank" rel="noopener noreferrer" className="d-flex align-items-center text-decoration-none text-dark footer-link">
                  <div className="social-icon-wrapper instagram-icon me-3">
                    <img src="\images\Instagram_icon.png.webp" alt="Instagram" className="img-fluid" />
                  </div>
                  <div>
                    <h6 className="mb-0 fw-bold social-text">Instagram</h6>
                    <p className="text-muted mb-0 small social-handle">@BookwormVirtual</p>
                  </div>
                </a>
              </li>
              <li className="mb-3">
                <a href="https://discord.gg/bookwormcommunity" target="_blank" rel="noopener noreferrer" className="d-flex align-items-center text-decoration-none text-dark footer-link">
                  <div className="social-icon-wrapper discord-icon me-3">
                    <img src="\images\discord_icon_130958.png" alt="Discord" className="img-fluid" />
                  </div>
                  <div>
                    <h6 className="mb-0 fw-bold social-text">Discord</h6>
                    <p className="text-muted mb-0 small social-handle">discord.gg/bookwormcommunity</p>
                  </div>
                </a>
              </li>
              <li className="mb-3">
                <a href="https://x.com/BookwormReads" target="_blank" rel="noopener noreferrer" className="d-flex align-items-center text-decoration-none text-dark footer-link">
                  <div className="social-icon-wrapper twitter-x-icon me-3">
                    <img src="\images\images.png" alt="Twitter/X" className="img-fluid" />
                  </div>
                  <div>
                    <h6 className="mb-0 fw-bold social-text">Twitter/X</h6>
                    <p className="text-muted mb-0 small social-handle">@BookwormReads</p>
                  </div>
                </a>
              </li>
            </ul>
          </div>

          {/* Middle Section: Company Logo, Name, and Description */}
          <div className="col-md-4 text-center d-flex flex-column justify-content-start align-items-center mb-4 mb-md-0 ">
            <div className="d-flex align-items-center">
          <Link
            to="/"
            className="d-flex align-items-center text-decoration-none"
          >
            <div
              className="loader me-2"
              style={{ width: "40px", height: "40px" }}
            >
              <BookLoader/>
            </div>
            <span className="fw-bold fs-4 text-pink">Bookworm</span>
          </Link>
        </div>
            <p className="text-muted text-center mt-3">
              Your gateway to a world of digital content - books, audiobooks, music, and short films.
            </p>
          </div>

          {/* Right Section: Visit Us & Get In Touch */}
          <div className="col-md-4 d-flex flex-column align-items-end text-end">
            <div className="mb-4">
              <h5 className="fw-bold mb-2 footer-heading">Visit Us</h5>
              <p className="text-muted mb-1">Bookworm HQ</p>
              <p className="text-muted mb-1">C-DAC SM VITA</p>
              <p className="text-muted mb-0">Mumbai, India</p>
            </div>
            <div>
              <h5 className="fw-bold mb-2 footer-heading">Get in Touch</h5>
              <p className="text-muted mb-1">
                <a href="mailto:bookworm.virtual@gmail.com" className="text-decoration-none text-muted footer-link">
                  bookworm.virtual@gmail.com
                </a>
              </p>
              <button className="btn btn-dark mt-3">Contact Us</button>
            </div>
          </div>
        </div>
        <hr className="my-4" />
        <div className="text-center text-muted">
          © 2025 Bookworm.com. All rights reserved.
        </div>
      </div>
    </footer>
  );
};

export default Footer;
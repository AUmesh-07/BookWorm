import React from 'react';
import './PremiumSection.css'; // Create this CSS file

const PremiumSection = () => {
  return (
    <section className="premium-section py-5">
      <div className="container text-center">
        <h2 className="mb-3 fw-bold text-purple">Join Bookworm Premium</h2>
        <p className="lead mb-4">
          Unlimited access to books, audiobooks, music, and short films for just $9.99/month.
        </p>
        <button className="btn btn-pink btn-lg">Start Free Trial</button>
      </div>
    </section>
  );
};

export default PremiumSection;
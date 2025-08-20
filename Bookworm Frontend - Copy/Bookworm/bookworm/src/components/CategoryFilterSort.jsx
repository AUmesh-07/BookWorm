import React, { useState } from 'react';
import './CategoryFilterSort.css'; // New CSS file for this component

const CategoryFilterSort = ({ onSearch, onSort, viewMode, onViewModeChange }) => {
  const [searchText, setSearchText] = useState('');

  const handleInputChange = (e) => {
    setSearchText(e.target.value);
    onSearch(e.target.value); // Trigger search on input change
  };

  const handleSortChange = (e) => {
    onSort(e.target.value);
  };

  return (
    <div className="category-filter-sort row align-items-center mb-4 p-3 bg-white rounded shadow-sm">
      <div className="col-lg-6 col-md-12 mb-3 mb-lg-0">
        <div className="input-group">
          <span className="input-group-text bg-transparent border-end-0">
            <i className="bi bi-search text-muted"></i>
          </span>
          <input
            type="text"
            className="form-control border-start-0 ps-0"
            placeholder="Search by title, author..."
            value={searchText}
            onChange={handleInputChange}
          />
        </div>
      </div>
      <div className="col-lg-6 col-md-12 d-flex justify-content-md-end align-items-center">
        <button className="btn btn-outline-secondary me-3 d-flex align-items-center">
          <i className="bi bi-funnel me-2"></i> Filter
        </button>
        <div className="dropdown">
          <select className="form-select" onChange={handleSortChange}>
            <option value="popularity">Sort by: Popularity</option>
            <option value="priceAsc">Price: Low to High</option>
            <option value="priceDesc">Price: High to Low</option>
            <option value="titleAsc">Title: A-Z</option>
            {/* Add more sort options */}
          </select>
        </div>
      </div>
    </div>
  );
};

export default CategoryFilterSort;
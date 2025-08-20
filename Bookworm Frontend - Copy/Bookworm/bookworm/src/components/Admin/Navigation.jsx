import React from 'react';
import './Admin.css';

export default function Navigation({ activeTab, setActiveTab }) {
  return (
    <div className="d-flex flex-column p-3 bg-light vh-100 shadow" style={{ width: '250px' }}>
      <h4 className="text-center mb-4">Admin Panel</h4>
      <ul className="nav nav-pills flex-column mb-auto">
        {['Products'].map(tab => (
          <li className="nav-item" key={tab}>
            <button
              className={`nav-link ${activeTab === tab ? 'active' : ''}`}
              onClick={() => setActiveTab(tab)}
            >
              {tab}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

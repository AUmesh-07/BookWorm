import React, { useState } from 'react';
import Navigation from './Navigation';
import Product from './Product';
import './Admin.css';
// Future: import Genre from './components/Genre'; etc.

export default function Admin() {
  const [activeTab, setActiveTab] = useState('Products');

  const renderTab = () => {
    switch (activeTab) {
      case 'Products':
        return <Product />;
      // case 'Genres':
      //   return <Genre />;
      // Add more tabs as needed
      default:
        return <div>Select a tab</div>;
    }
  };

  return (
    <div className="d-flex">
      <Navigation activeTab={activeTab} setActiveTab={setActiveTab} />
      <div className="container-fluid p-4">
        {renderTab()}
      </div>
    </div>
  );
}
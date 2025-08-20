import React, { useState } from 'react';
import './MyLibraryPage.css';
import LibraryCard from '../components/LibraryCard';
import Footer from '../components/Footer';

// Sample data for the library items
const libraryData = {
  rented: [
    {
      id: 1,
      type: 'Ebook',
      title: 'The Silent Patient',
      author: 'Alex Michaelides',
      image: 'http://googleusercontent.com/file_content/0',
      dueDate: 'Nov 15, 2023',
      status: 'active',
    },
    {
      id: 2,
      type: 'Audiobook',
      title: 'Educated',
      author: 'Tara Westover',
      image: 'http://googleusercontent.com/file_content/2',
      dueDate: 'Nov 25, 2023',
      status: 'active',
    },
    {
      id: 3,
      type: 'Audiobook',
      title: 'The Dutch House',
      author: 'Ann Patchett',
      image: 'http://googleusercontent.com/file_content/1',
      dueDate: 'Nov 15, 2023',
      status: 'expired',
    },
    {
      id: 4,
      type: 'Music',
      title: 'Evermore',
      author: 'Taylor Swift',
      image: 'http://googleusercontent.com/file_content/1',
      dueDate: 'Dec 1, 2023',
      status: 'active',
    }
  ],
  borrowed: [
    {
      id: 1,
      type: 'Ebook',
      title: 'The Silent Patient',
      author: 'Alex Michaelides',
      image: 'http://googleusercontent.com/file_content/0',
      dueDate: 'Nov 15, 2023',
      status: 'active',
    },
    {
      id: 2,
      type: 'Ebook',
      title: 'Where the Crawdads Sing',
      author: 'Delia Owens',
      image: 'http://googleusercontent.com/file_content/0',
      dueDate: 'Nov 3, 2023',
      status: 'overdue',
    },
  ]
};

const MyLibraryPage = () => {
  const [activeTab, setActiveTab] = useState('rented');
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredItems = libraryData[activeTab].filter(item =>
    item.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
    item.author.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="my-library-page container py-5">
      <div className="text-center text-md-start mb-4">
        <h1 className="fw-bold">My Library</h1>
        <p className="text-muted">Manage your borrowed and rented items</p>
      </div>

      <div className="search-bar mb-4">
        <div className="input-group">
          <span className="input-group-text bg-transparent border-end-0">
            <i className="bi bi-search text-muted"></i>
          </span>
          <input
            type="text"
            className="form-control border-start-0 ps-0"
            placeholder="Search your library..."
            value={searchTerm}
            onChange={handleSearch}
          />
        </div>
      </div>

      <div className="library-tabs d-flex border-bottom mb-4">
        <button
          className={`btn btn-link nav-link ${activeTab === 'rented' ? 'active-tab' : 'text-muted'}`}
          onClick={() => setActiveTab('rented')}
        >
          Rented ({libraryData.rented.length})
        </button>
        <button
          className={`btn btn-link nav-link ${activeTab === 'borrowed' ? 'active-tab' : 'text-muted'}`}
          onClick={() => setActiveTab('borrowed')}
        >
          Borrowed ({libraryData.borrowed.length})
        </button>
      </div>

      <div className="library-content">
        {filteredItems.length > 0 ? (
          filteredItems.map(item => (
            <LibraryCard key={item.id} item={item} />
          ))
        ) : (
          <p className="text-center text-muted">No items found.</p>
        )}
      </div>
      <Footer/>
    </div>
  );
};

export default MyLibraryPage;
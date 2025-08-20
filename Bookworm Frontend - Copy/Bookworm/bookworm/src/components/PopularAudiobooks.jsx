import React from 'react';
import BookCard from './BookCard'; // Reusing BookCard
import './ContentCarousel.css'; // Reusing the same carousel CSS

const popularAudiobooksData = [
  { id: 1, image: 'https://via.placeholder.com/300x400?text=Audiobook+1', type: 'Audiobook', title: 'The Alchemist', author: 'Paulo Coelho', price: 18.50 },
  { id: 2, image: 'https://via.placeholder.com/300x400?text=Audiobook+2', type: 'Audiobook', title: 'Becoming', author: 'Michelle Obama', price: 22.00 },
  { id: 3, image: 'https://via.placeholder.com/300x400?text=Audiobook+3', type: 'Audiobook', title: 'Where the Crawdads Sing', author: 'Delia Owens', price: 16.99 },
  { id: 4, image: 'https://via.placeholder.com/300x400?text=Audiobook+4', type: 'Audiobook', title: 'Educated', author: 'Tara Westover', price: 19.99 },
  { id: 5, image: 'https://via.placeholder.com/300x400?text=Audiobook+5', type: 'Audiobook', title: 'Greenlights', author: 'Matthew McConaughey', price: 21.00 },
];

const PopularAudiobooks = () => {
  return (
    <section className="popular-audiobooks-section py-5 bg-white">
      <div className="container">
        <h2 className="mb-4 fw-bold">Popular Audiobooks</h2>
        <div className="content-carousel">
          {popularAudiobooksData.map(audiobook => (
            <div className="carousel-item-wrapper" key={audiobook.id}>
              <BookCard
                image={audiobook.image}
                type={audiobook.type}
                title={audiobook.title}
                author={audiobook.author}
                price={audiobook.price}
              />
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default PopularAudiobooks;
import React from 'react';
import BookCard from './BookCard'; // Reusing BookCard
import './ContentCarousel.css'; // Reusing the same carousel CSS

const trendingMusicData = [
  { id: 1, image: 'https://via.placeholder.com/300x400?text=Music+Album+1', type: 'Music', title: 'Folklore', author: 'Taylor Swift', price: 9.99 },
  { id: 2, image: 'https://via.placeholder.com/300x400?text=Music+Album+2', type: 'Music', title: 'Sour', author: 'Olivia Rodrigo', price: 8.99 },
  { id: 3, image: 'https://via.placeholder.com/300x400?text=Music+Album+3', type: 'Music', title: 'Happier Than Ever', author: 'Billie Eilish', price: 10.99 },
  { id: 4, image: 'https://via.placeholder.com/300x400?text=Music+Album+4', type: 'Music', title: 'Divide', author: 'Ed Sheeran', price: 7.99 },
  { id: 5, image: 'https://via.placeholder.com/300x400?text=Music+Album+5', type: 'Music', title: 'After Hours', author: 'The Weeknd', price: 11.99 },
];

const TrendingMusic = () => {
  return (
    <section className="trending-music-section py-5 bg-light">
      <div className="container">
        <h2 className="mb-4 fw-bold">Trending Music</h2>
        <div className="content-carousel">
          {trendingMusicData.map(music => (
            <div className="carousel-item-wrapper" key={music.id}>
              <BookCard
                image={music.image}
                type={music.type}
                title={music.title}
                author={music.author}
                price={music.price}
              />
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default TrendingMusic;
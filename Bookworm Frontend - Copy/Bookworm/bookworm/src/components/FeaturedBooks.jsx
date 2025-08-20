import React, { useEffect, useState } from 'react';
import { Container, Row, Col, Spinner } from 'react-bootstrap';
import BookCard from './BookCard';

function FeaturedBooks({ onAddToCart, onCardClick }) {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchBooks() {
      try {
        const res = await fetch('https://localhost:7261/api/v1/products');
        const data = await res.json();

        // Pick 8 random books
        const shuffled = data.sort(() => 0.5 - Math.random());
        setBooks(shuffled.slice(0, 8));
      } catch (error) {
        console.error('Error fetching books:', error);
      } finally {
        setLoading(false);
      }
    }
    fetchBooks();
  }, []);

  if (loading) {
    return (
      <div className="text-center my-5">
        <Spinner animation="border" />
      </div>
    );
  }

  return (
    <Container className="mt-4">
      <h2 className="mb-4">Books You May Like</h2>
      <Row>
        {books.map(book => (
          <Col key={book.id} sm={6} md={3}>
            <BookCard
              book={book}
              onAddToCart={onAddToCart}
              onCardClick={onCardClick}
            />
          </Col>
        ))}
      </Row>
    </Container>
  );
}

export default FeaturedBooks;
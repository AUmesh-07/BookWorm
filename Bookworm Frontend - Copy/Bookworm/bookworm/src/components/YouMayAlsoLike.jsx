import React, { useEffect, useState } from "react";

import { Container, Row, Col } from "react-bootstrap";

import BookCard from "./BookCard";

import BookLoader from "./BookLoader";

import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7261/api/v1",
});

function YouMayAlsoLike({ onAddToCart, onCardClick, cartItemIds }) {
  const [books, setBooks] = useState([]);

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const { data: allBooks } = await axiosInstance.get("/products");

        let availableBooks = allBooks; // ✅ FIX: Only filter if cartItemIds is a valid Set

        if (cartItemIds instanceof Set) {
          availableBooks = allBooks.filter((book) => !cartItemIds.has(book.id));
        }

        const shuffled = availableBooks.sort(() => 0.5 - Math.random());

        setBooks(shuffled.slice(0, 4));
      } catch (error) {
        console.error("Error fetching books:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchBooks();
  }, [cartItemIds]);

  if (loading) {
    return (
      <div className="text-center my-5">
        <BookLoader />
      </div>
    );
  }

  if (!books.length) {
    return null; // Don't render the section if there are no books to show
  }

  return (
    <Container className="my-5">
       <h2 className="mb-4 fw-bold">You May Also Like</h2>{" "}
      <Row xs={1} sm={2} md={3} lg={4} className="g-4">
        {" "}
        {books.map((book) => (
          <Col key={book.id}>
            {" "}
            <BookCard
              book={book}
              onAddToCart={onAddToCart}
              onCardClick={onCardClick}
              isInCart={false}
            />
            {" "}
          </Col>
        ))}
        {" "}
      </Row>
      {" "}
    </Container>
  );
}

export default YouMayAlsoLike;

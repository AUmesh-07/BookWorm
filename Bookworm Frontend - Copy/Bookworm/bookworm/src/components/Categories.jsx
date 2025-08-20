import React, { useState, useEffect } from "react";
import axios from "axios"; // 1. IMPORT AXIOS
import "./Categories.css";

// 2. IMPORT THE BOOKCARD COMPONENT
import BookCard from "./BookCard";
// 3. IMPORT THE MODALS FROM CATEGORIESPAGE
import BookDetailModal from "../components/BookDetailModal";
import AddToCartModal from "../components/AddToCartModal";

// --- Define Genre Data ---
const genres = [
  {
    id: 1,
    genreId: 1,
    title: "Fiction",
    description: "Explore magical worlds and epic quests.",
    icon: (
      <img
        src="/images/Fiction Logo.png"
        alt="Poetry"
        className="genre-icon-image"
      />
    ),
    colorClass: "text-primary",
  },
  {
    id: 2,
    genreId: 2,
    title: "Non-Fiction",
    description: "Discover facts, history, and real-life stories.",
    icon: (
      <img
        src="/images/Non Fiction Logo.png"
        alt="Non-Fiction"
        className="genre-icon-image"
      />
    ),
    colorClass: "text-success",
  },
  {
    id: 3,
    genreId: 3,
    title: "Poetry",
    description: "Edge-of-your-seat suspense and mystery.",
    icon: <img src="/images/Poetry Logo.png" className="genre-icon-image" />,
    colorClass: "text-danger",
  },
  {
    id: 4,
    genreId: 4,
    title: "Novel",
    description: "Heart-warming tales of love and passion.",
    icon: <img src="/images/Novel Logo.png" className="genre-icon-image" />,
    colorClass: "text-warning",
  },
];

// --- Category Card Component ---
const CategoryCard = ({ icon, title, description, colorClass, onClick }) => (
  <div className="col-md-6 col-lg-3 mb-4" onClick={onClick}>
    <div className="card h-100 text-center border-0 shadow-sm category-card">
      <div className="card-body d-flex flex-column justify-content-center align-items-center">
        <div
          className={`category-icon-wrapper mb-3 ${colorClass.replace(
            "text-",
            "bg-soft-"
          )}`}
        >
          <span className={colorClass}>{icon}</span>
        </div>
        <h5 className="card-title fw-bold">{title}</h5>
        <p className="card-text text-muted">{description}</p>
      </div>
    </div>
  </div>
);

// --- AXIOS INSTANCE ---
const axiosInstance = axios.create({
  baseURL: "https://localhost:7261/api/v1",
});

axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// --- Main Component ---
const Categories = () => {
  const [selectedGenre, setSelectedGenre] = useState(null);
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [selectedBook, setSelectedBook] = useState(null);
  const [isCartModalOpen, setIsCartModalOpen] = useState(false);
  const [cartModalMode, setCartModalMode] = useState("buy");
  const [selectedBookForCart, setSelectedBookForCart] = useState(null);

  // Fetch products when genre changes
  useEffect(() => {
    if (!selectedGenre) {
      setProducts([]);
      return;
    }

    const fetchProductsByGenre = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await axiosInstance.get(
          `/products/category/${selectedGenre.genreId}`
        );
        setProducts(response.data);
      } catch (e) {
        setError(e.message);
        console.error("Failed to fetch products:", e);
      } finally {
        setLoading(false);
      }
    };

    fetchProductsByGenre();
  }, [selectedGenre]);

  const handleGenreClick = (genre) => {
    setSelectedGenre(genre);
  };

  const handleBackClick = () => {
    setSelectedGenre(null);
  };

  const handleCardClick = (bookId) => {
    const book = products.find((p) => p.id === bookId);
    setSelectedBook(book);
  };

  const openCartModal = (book, mode) => {
    const userStr = localStorage.getItem("user");
    if (!userStr || userStr === "undefined") {
      alert("You must be logged in to add items to the cart.");
      return;
    }
    const user = JSON.parse(userStr);
    if (!user.id) {
      alert("Could not find your user ID. Please try logging in again.");
      return;
    }
    setSelectedBookForCart(book);
    setCartModalMode(mode);
    setIsCartModalOpen(true);
  };

  const confirmAddToCart = async ({ quantity, rentNumberOfDays }) => {
    const user = JSON.parse(localStorage.getItem("user"));
    const customerId = user.id;

    const payload = {
      productId: selectedBookForCart.id,
      quantity: quantity,
      isRented: cartModalMode === "rent",
      rentNumberOfDays: rentNumberOfDays,
    };

    try {
      await axiosInstance.post(`/carts/${customerId}/items`, payload);
      alert(
        `Success! "${selectedBookForCart.name}" has been added to your cart.`
      );
    } catch (err) {
      console.error("Failed to add item to cart:", err);
      if (
        err.response &&
        (err.response.status === 401 || err.response.status === 403)
      ) {
        alert("Your session has expired. Please log in again.");
      } else {
        alert(
          "There was an error adding the item to your cart. Please try again."
        );
      }
    } finally {
      setIsCartModalOpen(false);
    }
  };

  return (
    <section className="categories-section py-5 bg-white">
      <div className="container">
        {selectedGenre ? (
          <>
            <div className="d-flex align-items-center mb-5">
              <button
                className="btn btn-outline-secondary me-3"
                onClick={handleBackClick}
              >
                &larr; Back to Genres
              </button>
              <h2 className="fw-bold mb-0">
                Genre:{" "}
                <span className={selectedGenre.colorClass}>
                  {selectedGenre.title}
                </span>
              </h2>
            </div>

            {loading && (
              <div className="text-center">
                <p>Loading...</p>
              </div>
            )}
            {error && <div className="alert alert-danger">Error: {error}</div>}

            {!loading && !error && (
              <div className="row">
                {products.length > 0 ? (
                  products.map((product) => (
                    <div key={product.id} className="col-md-4 col-lg-3 mb-4">
                      <BookCard
                        book={product}
                        onAddToCart={openCartModal}
                        onCardClick={() => handleCardClick(product.id)}
                      />
                    </div>
                  ))
                ) : (
                  <div className="col-12">
                    <p className="text-center text-muted">
                      No products found for this genre.
                    </p>
                  </div>
                )}
              </div>
            )}
          </>
        ) : (
          <>
            <h2 className="text-center mb-5 fw-bold">Browse by Genre</h2>
            <div className="row justify-content-center">
              {genres.map((genre) => (
                <CategoryCard
                  key={genre.id}
                  {...genre}
                  onClick={() => handleGenreClick(genre)}
                />
              ))}
            </div>
          </>
        )}
      </div>

      {isCartModalOpen && selectedBookForCart && (
        <AddToCartModal
          book={selectedBookForCart}
          mode={cartModalMode}
          onClose={() => setIsCartModalOpen(false)}
          onConfirm={confirmAddToCart}
        />
      )}

      {selectedBook && (
        <BookDetailModal
          book={selectedBook}
          onClose={() => setSelectedBook(null)}
        />
      )}
    </section>
  );
};

export default Categories;

import React, { useState, useEffect, useCallback } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import axios from "axios";
import Footer from "../components/Footer";
import BookCard from "../components/BookCard";
import BookDetailModal from "../components/BookDetailModal";
import AddToCartModal from "../components/AddToCartModal";

// --- Use the URL from the second file ---
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

const BOOKS_PER_PAGE = 10;

const CategoriesPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const query = new URLSearchParams(location.search);

  // States for URL-based filters and search
  const [searchTerm, setSearchTerm] = useState(query.get("query") || "");
  const [searchType, setSearchType] = useState(query.get("searchType") || "name");
  const [genreId, setGenreId] = useState(query.get("genreId") || "");
  const [languageId, setLanguageId] = useState(query.get("languageId") || "");
  const [typeId, setTypeId] = useState(query.get("typeId") || "");

  // --- States for pagination ---
  const [allFilteredBooks, setAllFilteredBooks] = useState([]); // All books after filtering
  const [booksToDisplay, setBooksToDisplay] = useState([]); // Books currently visible
  const [page, setPage] = useState(1);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  
  // States for modals and cart
  const [selectedBook, setSelectedBook] = useState(null);
  const [isCartModalOpen, setIsCartModalOpen] = useState(false);
  const [cartModalMode, setCartModalMode] = useState("buy");
  const [selectedBookForCart, setSelectedBookForCart] = useState(null);
  const [cartItemIds, setCartItemIds] = useState(new Set());
  const user = JSON.parse(localStorage.getItem("user") || "{}");
  const customerId = user.id;

  // Sync state with URL on initial load and changes
  useEffect(() => {
    const q = new URLSearchParams(location.search);
    setSearchTerm(q.get("query") || "");
    setSearchType(q.get("searchType") || "name");
    setGenreId(q.get("genreId") || "");
    setLanguageId(q.get("languageId") || "");
    setTypeId(q.get("typeId") || "");
  }, [location.search]);

  // Update URL whenever a filter or search term changes
  useEffect(() => {
    const params = new URLSearchParams();
    if (searchTerm) params.set("query", searchTerm);
    if (searchType) params.set("searchType", searchType);
    if (genreId) params.set("genreId", genreId);
    if (languageId) params.set("languageId", languageId);
    if (typeId) params.set("typeId", typeId);
    navigate({ pathname: "/CategoriesPage", search: params.toString() }, { replace: true });
  }, [searchTerm, searchType, genreId, languageId, typeId, navigate]);

  // --- Effect to fetch and filter books based on all criteria ---
  const fetchAndFilterBooks = useCallback(async () => {
    setIsLoading(true);
    setError("");
    try {
      // Always fetch all products from the base endpoint for client-side filtering
      const { data: allProducts } = await axiosInstance.get("/products");

      let filteredProducts = allProducts;
      
      // Apply filters based on URL parameters
      if (genreId) {
        filteredProducts = filteredProducts.filter(p => p.genreId?.toString() === genreId);
      }
      if (languageId) {
        filteredProducts = filteredProducts.filter(p => p.languageId?.toString() === languageId);
      }
      if (typeId) {
        filteredProducts = filteredProducts.filter(p => p.productTypeId?.toString() === typeId);
      }
      if (searchTerm) {
        const queryTerm = searchTerm.trim().toLowerCase();
        if (searchType === "name") {
          filteredProducts = filteredProducts.filter(p => p.name.toLowerCase().includes(queryTerm));
        } else if (searchType === "author") {
          filteredProducts = filteredProducts.filter(p => p.author?.toLowerCase().includes(queryTerm));
        }
      }

      setAllFilteredBooks(filteredProducts);
      // Reset pagination to show the first page of the new filtered results
      setPage(1);
      setBooksToDisplay(filteredProducts.slice(0, BOOKS_PER_PAGE));
    } catch (err) {
      setError("Failed to fetch products.");
      setAllFilteredBooks([]);
      setBooksToDisplay([]);
    } finally {
      setIsLoading(false);
    }
  }, [searchTerm, searchType, genreId, languageId, typeId]);

  // Trigger the fetch and filter logic whenever a dependency changes
  useEffect(() => {
    fetchAndFilterBooks();
  }, [fetchAndFilterBooks]);

  // --- Effect to handle cart state ---
  useEffect(() => {
    const fetchCart = async () => {
      if (!customerId) return;
      try {
        const { data } = await axiosInstance.get(`/carts/${customerId}`);
        const ids = new Set(data.items.map(item => item.productId));
        setCartItemIds(ids);
      } catch (err) {
        console.error("Could not fetch cart on page load:", err);
      }
    };
    fetchCart();
  }, [customerId]);

  const openCartModal = (book, mode) => {
    if (!customerId) {
      toast.error("You must be logged in to add items to the cart.");
      return;
    }
    setSelectedBookForCart(book);
    setCartModalMode(mode);
    setIsCartModalOpen(true);
  };

  const confirmAddToCart = async ({ quantity, rentNumberOfDays }) => {
    const payload = {
      productId: selectedBookForCart.id,
      quantity: 1,
      isRented: cartModalMode === "rent",
      rentNumberOfDays: cartModalMode === "rent" ? rentNumberOfDays : null,
    };

    try {
      await axiosInstance.post(`/carts/${customerId}/items`, payload);
      toast.success(`"${selectedBookForCart.name}" added to cart!`);
      setCartItemIds(prevIds => new Set(prevIds).add(selectedBookForCart.id));
    } catch (err) {
      const message = err.response?.data?.message || err.message;
      if (message.includes("Product is already in your cart")) {
        toast.warn("This item is already in your cart.");
      } else {
        toast.error("An error occurred. Please try again.");
      }
    } finally {
      setIsCartModalOpen(false);
    }
  };

  const handleCardClick = async (bookId) => {
    try {
      const { data } = await axiosInstance.get(`/products/${bookId}`);
      setSelectedBook(data);
    } catch (err) {
      toast.error("Failed to load book details");
    }
  };

  // --- Handle "Load More" button click ---
  const handleLoadMore = () => {
    const newPage = page + 1;
    const startIndex = page * BOOKS_PER_PAGE;
    const endIndex = startIndex + BOOKS_PER_PAGE;
    setBooksToDisplay(prevBooks => [...prevBooks, ...allFilteredBooks.slice(startIndex, endIndex)]);
    setPage(newPage);
  };

  const hasMoreBooks = booksToDisplay.length < allFilteredBooks.length;

  return (
    <div className="categories-page container my-5">
      <ToastContainer position="top-center" />
      <div className="text-center text-md-start mb-4">
        <h1 className="fw-bold">Search Collection</h1>
        <p className="text-muted">Discover books and audiobooks by name or author.</p>
      </div>

      {/* Search and Filter UI */}
      <div className="row mb-4 g-3">
        <div className="col-md-4">
          <input
            type="text"
            className="form-control"
            placeholder={`Search by ${searchType}...`}
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="col-md-2">
          <select className="form-select" value={searchType} onChange={(e) => setSearchType(e.target.value)}>
            <option value="name">By Name</option>
            <option value="author">By Author</option>
          </select>
        </div>
        <div className="col-md-2">
          <select className="form-select" value={genreId} onChange={(e) => setGenreId(e.target.value)}>
            <option value="">All Genres</option>
            <option value="1">Fiction</option>
            <option value="2">Non-Fiction</option>
            <option value="3">Poetry</option>
            <option value="4">Graphic Novel</option>
          </select>
        </div>
        <div className="col-md-2">
          <select className="form-select" value={languageId} onChange={(e) => setLanguageId(e.target.value)}>
            <option value="">All Languages</option>
            <option value="1">English</option>
            <option value="2">Hindi</option>
            <option value="3">Marathi</option>
          </select>
        </div>
        <div className="col-md-2">
          <select className="form-select" value={typeId} onChange={(e) => setTypeId(e.target.value)}>
            <option value="">All Types</option>
            <option value="1">Ebook</option>
            <option value="2">Audiobook</option>
          </select>
        </div>
      </div>
      
      {error && <div className="alert alert-danger">{error}</div>}
      {isLoading && <div className="text-center"><p>Loading...</p></div>}
      
      <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 row-cols-xl-5 g-4">
        {!isLoading && booksToDisplay.map((item) => (
          <div key={item.id} className="col">
            <BookCard
              book={item}
              onAddToCart={openCartModal}
              onCardClick={handleCardClick}
              isInCart={cartItemIds.has(item.id)}
            />
          </div>
        ))}
      </div>

      {!isLoading && !booksToDisplay.length && (
        <div className="text-center p-5">
          <p className="text-muted fs-4">No results found for your query.</p>
        </div>
      )}
      
      {/* --- Load More Button --- */}
      {!isLoading && hasMoreBooks && (
        <div className="text-center my-5">
          <button className="btn btn-primary btn-lg" onClick={handleLoadMore}>
            Load More Books
          </button>
        </div>
      )}

      {isCartModalOpen && selectedBookForCart && (
        <AddToCartModal
          book={selectedBookForCart}
          mode={cartModalMode}
          onClose={() => setIsCartModalOpen(false)}
          onConfirm={confirmAddToCart}
        />
      )}
      
      {selectedBook && (
        <BookDetailModal book={selectedBook} onClose={() => setSelectedBook(null)} />
      )}
      
      <Footer />
    </div>
  );
};

export default CategoriesPage;

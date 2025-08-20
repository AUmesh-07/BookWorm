import React, { useEffect, useState } from "react";
import ProductForm from "./ProductForm";
import ProductTable from "./ProductTable";
import axios from "axios";

export default function Product() {
  const [products, setProducts] = useState([]);
  const [selectedProduct, setSelectedProduct] = useState(null);

  const genres = [
    { id: 1, name: "Fiction" },
    { id: 2, name: "Non-Fiction" },
    { id: 3, name: "Poetry" },
    { id: 4, name: "Graphic Novels" },
  ];

  const languages = [
    { id: 1, name: "English" },
    { id: 2, name: "Hindi" },
    { id: 3, name: "Marathi" },
  ];

  const types = [
    { id: 1, name: "Ebook" },
    { id: 2, name: "Audiobook" },
  ];

  const fetchProducts = async () => {
    try {
      const res = await axios.get("https://localhost:7261/api/v1/products");
      setProducts(res.data);
    } catch (err) {
      console.error("Failed to fetch products:", err);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  const handleSave = async (product) => {
    try {
      if (selectedProduct) {
        await axios.put(
          `https://localhost:7261/api/v1/products/${selectedProduct.id}`,
          product
        );
      } else {
        await axios.post("https://localhost:7261/api/v1/products", product);
      }
      fetchProducts();
      setSelectedProduct(null);
    } catch (err) {
      console.error("Error saving product:", err);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Delete this product?")) return;

    try {
      await axios.delete(`https://localhost:7261/api/v1/products/${id}`);
      fetchProducts();
    } catch (err) {
      console.error("Failed to delete product:", err);
    }
  };

  return (
    <>
      <ProductForm
        genres={genres}
        languages={languages}
        types={types}
        onSave={handleSave}
        selected={selectedProduct}
        clearForm={() => setSelectedProduct(null)}
      />
      <ProductTable
        products={products}
        onEdit={setSelectedProduct}
        onDelete={handleDelete}
      />
    </>
  );
}

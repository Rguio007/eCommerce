import { useState, useEffect } from "react";
import ProductRoster from "../components/ProductRoster";
import ProductService from "../services/ProductService";
import { Container, Box } from "@mui/material";

function Home() {
  const [searchTerm, setSearchTerm] = useState("");
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const productsData = await ProductService.getAllProducts();
        setProducts(productsData);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchProducts();
  }, []);

  const handleSearch = (searchValue) => {
    setSearchTerm(searchValue);
    /*const filtered = charactersArr.filter((character) =>
      character.name.toLowerCase().includes(searchValue.toLowerCase())
    );
    setFilteredCharacters(filtered);*/
  };
  return (
    <>
      <Container>
        <Box sx={{ flexGrow: 1 }}>
          <input
            type="text"
            className="search-bar"
            placeholder="Search by name"
            aria-label="Search by name"
            value={searchTerm}
            onChange={(e) => handleSearch(e.target.value)}
          />
        </Box>
        <ProductRoster products={products} />
      </Container>
    </>
  );
}

export default Home;

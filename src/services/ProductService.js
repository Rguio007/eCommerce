import axios from 'axios';

const BASE_URL = 'https://localhost:7089/api/products';

const ProductService = {
  getAllProducts: async () => {
    try {
      const response = await axios.get(`${BASE_URL}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching products:', error);
      throw error;
    }
  },
};

export default ProductService;

import axios from "axios";

const ServerAddress = "http://localhost:5120/api/Category";

export const getAllCategoriesFromDB = async () => {
  try {
    console.log("servicess - CategoryService - getAllCategoriesFromDB ran Successfully");
    let endpoint = await axios.get(`${ServerAddress}/getAllCategoriesFromDB`);
    return endpoint.data;
  } catch (ex) {
    console.log(`An Exception occurred while initializing the getAllNewsCardsFromDB Service : ${ex}`);
  }
};

export const getUserCategoriesFromDB = async () => {
  try {
    console.log("servicess - CategoryService - getUserCategoriesFromDB ran Successfully");
    let endpoint = await axios.get(`${ServerAddress}/getUserCategoriesFromDB`);
    return endpoint.data;
  } catch (ex) {
    console.log(`An Exception occurred while initializing the getUserCategoriesFromDB Service : ${ex}`);
  }
};

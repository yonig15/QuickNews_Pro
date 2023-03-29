import axios from "axios";

const ServerAddress = "http://localhost:5120/api/NewsItem";

export const getAllNewsCardsFromDB = async () => {
  try {
    console.log("servicess - NewsService - getAllNewsCardsFromDB ran Successfully");
    let endpoint = await axios.get(`${ServerAddress}/getAllNewsCardsFromDB`);
    return endpoint.data;
  } catch (ex) {
    console.log(`An Exception occurred while initializing the getAllNewsCardsFromDB Service : ${ex}`);
  }
};

export const getUserNewsCardsFromDB = async () => {
  try {
    console.log("servicess - NewsService - getUserNewsCardsFromDB ran Successfully");
    let endpoint = await axios.get(`${ServerAddress}/getUserNewsCardsFromDB`);
    return endpoint.data;
  } catch (ex) {
    console.log(`An Exception occurred while initializing the getUserNewsCardsFromDB Service : ${ex}`);
  }
};

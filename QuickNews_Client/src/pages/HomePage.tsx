import React, { useState, useEffect } from "react";
import NewsCard from "../components/singleNewsCard/NewsCard";
import { useAuth0 } from "@auth0/auth0-react";
import { getAllNewsCardsFromDB, getUserNewsCardsFromDB } from "../servicess/NewsService";

export function HomePage() {
  const { isAuthenticated } = useAuth0();
  const [AllNewsCards, setAllNewsCards] = useState([]);
  const [UserNewsCards, setUserNewsCards] = useState([]);

  const getNewsCards = async () => {
    if (!isAuthenticated) {
      let result = await getAllNewsCardsFromDB();
      setAllNewsCards(result);
    } else {
      let result = await getUserNewsCardsFromDB();
      setUserNewsCards(result);
    }
  };

  useEffect(() => {
    getNewsCards();
  }, []);

  return (
    <>
      {!isAuthenticated && AllNewsCards.length > 0 ? (
        AllNewsCards.map((NewsItem) => {
          let { ImageUrl, Title, Description, Link } = NewsItem;
          return (
            <>
              <NewsCard
                props={{
                  ImageUrl: ImageUrl,
                  Title: Title,
                  Description: Description,
                  Link: Link,
                }}
              />
            </>
          );
        })
      ) : (
        <>{<h1>There are no News.</h1>}</>
      )}
      {isAuthenticated && UserNewsCards.length > 0 ? (
        UserNewsCards.map((NewsItem) => {
          let { ImageUrl, Title, Description, Link } = NewsItem;
          return (
            <>
              <NewsCard
                props={{
                  ImageUrl: ImageUrl,
                  Title: Title,
                  Description: Description,
                  Link: Link,
                }}
              />
            </>
          );
        })
      ) : (
        <>{<h1>There are no User News.</h1>}</>
      )}
    </>
  );
}

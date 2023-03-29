import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect, useState } from "react";
import { getAllCategoriesFromDB, getUserCategoriesFromDB } from "../servicess/CategoryService";

export function DefinitionsPage() {
  const { isAuthenticated } = useAuth0();
  const [AllCategories, setAllCategories] = useState([]);
  const [UserCategories, setUserCategories] = useState([]);

  const getCategories = async () => {
    if (!isAuthenticated) {
      let result = await getAllCategoriesFromDB();
      setAllCategories(result);
    } else {
      let result = await getUserCategoriesFromDB();
      setUserCategories(result);
    }
  };

  useEffect(() => {
    getCategories();
  }, []);

  return (
    <>
      {!isAuthenticated && AllCategories.length > 0 ? (
        AllCategories.map((Category) => {
          let { Topic } = Category;
          return (
            <>
              <div className="btn-group" role="group" aria-label="Basic checkbox toggle button group">
                <input type="checkbox" className="btn-check" id={`btncheck_${Topic}`} autoComplete="off"></input>
                <label className="btn btn-outline-danger" htmlFor={`btncheck_${Topic}`}>
                  {Topic}
                </label>
              </div>
            </>
          );
        })
      ) : (
        <>{<h1>There are no Categories.</h1>}</>
      )}
      {isAuthenticated && UserCategories.length > 0 ? (
        UserCategories.map((Category) => {
          let { Topic } = Category;
          return (
            <>
              <div className="btn-group" role="group" aria-label="Basic checkbox toggle button group">
                <input type="checkbox" className="btn-check" id={`btncheck_${Topic}`} autoComplete="off"></input>
                <label className="btn btn-outline-danger" htmlFor={`btncheck_${Topic}`}>
                  {Topic}
                </label>
              </div>
            </>
          );
        })
      ) : (
        <>{<h1>There are no User Categories.</h1>}</>
      )}
    </>
  );
}

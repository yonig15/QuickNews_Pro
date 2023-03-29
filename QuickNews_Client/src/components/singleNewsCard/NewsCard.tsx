import React from "react";
import "./NewsCardStyle.css";

function NewsCard(props: any) {
  return (
    <>
      <figure className="snip1369 green">
        <img src={props.ImageUrl} alt="pr-sample15" />
        <div className="image">
          <img src={props.ImageUrl} alt="pr-sample15" />
        </div>
        <figcaption>
          <h3>{props.Title}</h3>
          <p>{props.Description}</p>
        </figcaption>
        <span className="read-more">
          Read More <i className="ion-android-arrow-forward"></i>
        </span>
        <a href={props.Link}></a>
      </figure>
    </>
  );
}

export default NewsCard;

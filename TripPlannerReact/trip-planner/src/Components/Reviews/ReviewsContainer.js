import React, {useState, useEffect} from 'react';
import ReviewsTable from './ReviewsTable';

let UserProfile = (props) => {
    const [reviews, setReviews] = useState([]);

    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                //"Authorization": "Bearer " + props.token
            }
        };
        console.log(props.userID);
        fetch("https://localhost:44365/api/reviews/byuser/25f160d5-d3c6-4944-b3e5-a8d6d29831c8" /*+ props.userID*/, init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("reviews fetch failed")
                }
                return response.json();
            })
            .then(json => {setReviews(json)})
            .catch(console.log);
    }, [props.token]);

    let viewAddForm = (review) => {
        console.log("Adding review");
    }

    let viewUpdateForm = (review) => {
        console.log("Updating review");
    }

    let viewDeleteForm = (review) => {
        console.log("Removing review");
    }

    let viewViewForm = (review) => {
        console.log("Viewing review");
    }

    return (   
        <div className="container d-flex align-items-center justify-content-left profile-container">
            <div className="row">
                <div className="col">
                    <ReviewsTable 
                        list={reviews} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
            </div>
        </div>
    );
}

export default UserProfile;
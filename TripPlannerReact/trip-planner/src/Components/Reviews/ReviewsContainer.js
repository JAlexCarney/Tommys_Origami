import React, {useState, useEffect} from 'react';
import ReviewsTable from './ReviewsTable';
import ReviewsFormSelector from './ReviewsFormSelector';

let UserProfile = (props) => {
    const [state, setState] = useState({list:[], form:"", action:()=>{}, review:{}});

    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + props.token
            }
        };
        console.log(props.userID);
        fetch("https://localhost:44365/api/reviews/getreviewsbyuser/" + props.userID, init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("reviews fetch failed")
                }
                return response.json();
            })
            .then(json => {setState({
                list:json,
                form:"", 
                action:()=>{}, 
                review:{}
            })})
            .catch(console.log);
    }, [props.token]);

    let viewAddForm = (review) => {
        let newState = {...state};
        newState.form = "Add";
        newState.action = (review) => {
            let reviewWithUser = {...review};
            reviewWithUser.userID = props.userID;
            //console.log(reviewWithUser);
            const init = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(reviewWithUser)
              };
          
            fetch("https://localhost:44365/api/reviews", init)
                .then(response => {
                    if (response.status !== 201) {
                        return Promise.reject("response is not 200 OK");
                    }
                    return response.json();
                })
                .then((json) => {
                    let newState = {...state};
                    newState.list.unshift(json);
                    newState.form = "";
                    newState.action = ()=>{};
                    setState(newState);
                })
                .catch(console.log);
        };
        newState.review = review;
        setState(newState);
    }

    let editReviews = (id, list) => {
        for(let i = 0; i < list.length; i++){
            const init = {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(list[i])
              };
          
            fetch("https://localhost:44365/api/reviews", init)
                .then(response => {
                    if (response.status !== 200) {
                        console.log(response.status);
                        return Promise.reject("response is not 200 OK");
                    }
                    // return response.json();
                })
                .catch(console.log);
        }
    }

    let viewUpdateForm = (review) => {
        let newState = {...state};
        newState.form = "Edit";
        console.log(review);
        newState.action = (review, list) => {
            let reviewWithUser = {...review};
            reviewWithUser.userID = props.userID;
            console.log(reviewWithUser);
            const init = {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(reviewWithUser)
              };
          
            fetch("https://localhost:44365/api/reviews/", init)
                .then(response => {
                    if (response.status !== 200) {
                        console.log(response.status);
                        return Promise.reject("response is not 200 OK");
                    }
                    // return response.json();
                })
                .then(() => {
                    editReviews(review.userID, list);
                    let newState = {...state};
                    let i = state.list.findIndex(r => r.reviewID === review.reviewID)
                    newState.list = [...state.list];
                    newState.list[i] = review;
                    newState.form = "";
                    newState.action = ()=>{};
                    newState.review = {};
                    setState(newState);
                })
                .catch(console.log);
        };
        newState.review = review;
        setState(newState);
    }

    let viewDeleteForm = (review) => {
        let newState = {...state};
        newState.form = "Delete";
        newState.action = (review) => {
            console.log(review);
            const init = {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                },
                body: JSON.stringify(review)
              };
              
            console.log(review);
            fetch(`https://localhost:44365/api/reviews`, init)
                .then(response => {
                    if (response.status === 200) {
                        let newState = {...state};
                        newState.list = state.list.filter(r => r.destinationID  !== review.destinationID);
                        newState.form = "";
                        newState.action = ()=>{};
                        newState.review = {};
                        setState(newState);
                    }
                    return Promise.reject("response is not 200 OK");
                })
                .catch(console.log);
        };
        newState.review = review;
        setState(newState);
    }

    let viewViewForm = (review) => {
        let newState = {...state};
        newState.form = "View";
        newState.action = ()=>{};
        newState.review = review;
        setState(newState);
    }

    let exitView = () => {
        let newState = {...state};
        newState.form = "";
        newState.action = ()=>{};
        newState.trip = {};
        setState(newState);
    }

    let tableSize="col col-6";
    if(state.form === "")
    {
        tableSize="col col-12";
    }

    return (   
        <div className="container-flex profile-container">
            <div className="row">
                <div className="col col-6">
                    <ReviewsTable 
                        list={state.list} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
                <div className="col col-6">
                    <ReviewsFormSelector token={props.token} form={state.form} review={state.review} action={state.action} exitAction={exitView}/>
                </div>
            </div>
        </div>
    );
}

export default UserProfile;
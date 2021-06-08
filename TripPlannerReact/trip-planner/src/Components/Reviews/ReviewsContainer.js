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
            .then(json => {setState(json)})
            .catch(console.log);
    }, [props.token]);

    let viewAddForm = (review) => {
        let newState = {...state};
        newState.form = "Add";
        newState.action = (review) => {
            let reviewWithUser = {...review};
            reviewWithUser.userID = props.userID;
            console.log(reviewWithUser);
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
                    newState.trip = {};
                    setState(newState);
                })
                .catch(console.log);
        };
        newState.review = review;
        setState(newState);
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
        <div className="container d-flex align-items-center justify-content-left profile-container">
            <div className="row">
                <div className="col">
                    <ReviewsTable 
                        list={state.list} 
                        handleAdd={viewAddForm} 
                        handleUpdate={viewUpdateForm} 
                        handleDelete={viewDeleteForm} 
                        handleView={viewViewForm}
                        />
                </div>
                <div className="col col-6">
                    <ReviewsFormSelector form={state.form} review={state.review} action={state.action} exitAction={exitView}/>
                </div>
            </div>
        </div>
    );
}

export default UserProfile;
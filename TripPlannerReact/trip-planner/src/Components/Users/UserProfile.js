import React, {useState} from 'react';
import TripsContainer from '../Trips/TripsContainer';
import ReviewsContainer from '../Reviews/ReviewsContainer';
import ReportsContainer from '../Reports/ReportsContainer';

let UserProfile = (props) => {
    const [state, setState] = useState({page:"trips"});

    let PromptDeleteAccount = () => {
        if (window.confirm("Are you sure you want to delete your profile?")) {
            const init = {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + props.token
                }
              };
          
            fetch(`https://localhost:44365/api/users/${props.userID}`, init)
                .then(response => {
                    if (response.status !== 200) {
                        return Promise.reject("response is not 200 OK");
                    }
                })
                .catch(console.log);
            props.changePage("Login");
        }
    }

    let displayPage = () => {
        switch(state.page){
            case "trips":
                return <TripsContainer userID={props.userID} token={props.token}/>;
            case "reviews":
                return <ReviewsContainer userID={props.userID} token={props.token}/>;
            case "popular":
                return <ReportsContainer token={props.token}/>;
            default:
                return <></>;
        }
    }

    return (
        <>
            <header>
            <div className="container-flex my-navbar">
                <div className="row">
                    <div className="col col-2">
                        <span className="navbar-brand">TRIP PLANNER</span>
                    </div>
                    <div className="col col-4">
                        <button className="btn btn-secondary navbar-btn" onClick={() => {setState({page:"trips"})}}>Trips</button>
                        <button className="btn btn-secondary navbar-btn" onClick={() => {setState({page:"reviews"})}}>Reviews</button>
                        <button className="btn btn-secondary navbar-btn" onClick={() => {setState({page:"popular"})}}>Popular</button>
                    </div>
                    <div className="col col-6 text-end">
                        <button className="btn btn-primary navbar-btn" onClick={() => {props.changePage("Login");}}>Log out</button>
                        <button className="btn btn-primary navbar-btn" onClick={() => {props.changePage("EditUser");}}>Edit Account</button>
                        <button className="btn btn-danger navbar-btn" onClick={PromptDeleteAccount}>Delete Account</button>
                    </div>
                </div>
            </div>
            </header>
            {displayPage()}
        </>
    );
}

export default UserProfile;
import React from 'react';
import TripsContainer from '../Trips/TripsContainer';
import ReviewsContainer from '../Reviews/ReviewsContainer';

let UserProfile = (props) => {
    
    let PromptDeleteAccount = () => {

    }

    return (
        <>
            <header>
            <div className="container-flex my-navbar">
                <div className="row">
                    <div className="col col-6">
                        <span className="navbar-brand">TRIP PLANNER</span>
                    </div>
                    <div className="col col-6 text-end">
                        <button className="btn btn-primary navbar-btn" onClick={() => {props.changePage("Login");}}>Log out</button>
                        <button className="btn btn-primary navbar-btn" onClick={() => {props.changePage("EditUser");}}>Edit Account</button>
                        <button className="btn btn-danger navbar-btn" onClick={PromptDeleteAccount}>Delete Account</button>
                    </div>
                </div>
            </div>
            </header>
            <TripsContainer userID={props.userID} token={props.token}/>
        </>
    );
}

export default UserProfile;
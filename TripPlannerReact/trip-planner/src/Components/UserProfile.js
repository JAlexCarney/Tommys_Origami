import React from 'react';
import TripsContainer from './Trips/TripsContainer';

let UserProfile = (props) => {
    
    return (
        <div>
            <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3 my-navbar">
            <div className="container">
                <div className="row">
                <div className="col col-5">
                    <span className="navbar-brand">TRIP PLANNER</span>
                </div>
                <div className="col">
                    <button className="btn btn-primary" onClick={() => {props.changePage("Login");}}>Log out</button>
                    <button className="btn btn-primary" onClick={() => {props.changePage("Login");}}>Edit Account</button>
                </div>
                </div>
            </div>
            </nav>
            </header>
            <TripsContainer userID={props.userID} token={props.token}/>
        </div>
    );
}

export default UserProfile;
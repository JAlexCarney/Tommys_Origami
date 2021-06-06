import React, {useState} from 'react';
import Login from './Login';
import CreateUser from './CreateUser';
import UserProfile from './UserProfile';

let TripPlanner = () => {
    const [state, setState] = useState(
        {
            page:"Login",
            token:"",
            userID:""
        });

    function ChangePage(page){
        let newState = {...state}
        newState.page = page;
        setState(newState);
    }
    function SetTokenAndUserID(token, userID){
        let newState = {...state}
        newState.token = token;
        newState.userID = userID;
        newState.page = "UserProfile";
        console.log(newState.token);
        console.log(newState.userID);
        setState(newState);
    }

    switch(state.page){
        case "Login":
            return <div className="container-flex d-flex align-items-center justify-content-center login-container">
                        <Login changePage={ChangePage} setTokenAndUserID={SetTokenAndUserID}/>
                    </div>;
        case "CreateUser":
            return <div className="container-flex d-flex align-items-center justify-content-center login-container">
                        <CreateUser changePage={ChangePage} />
                    </div>;
        case "UserProfile":
            return <UserProfile changePage={ChangePage} token={state.token} userID={state.userID}/>;
    }
}

export default TripPlanner;
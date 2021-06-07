import React, {useState} from 'react';
import Login from './Login';
import CreateUser from './Users/CreateUser';
import EditUser from './Users/EditUser';
import UserProfile from './Users/UserProfile';

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
        setState(newState);
    }

    switch(state.page){
        
        case "CreateUser":
            return <div className="container-flex d-flex align-items-center justify-content-center login-container">
                        <CreateUser changePage={ChangePage} />
                    </div>;
        case "EditUser":
            return <div className="container-flex d-flex align-items-center justify-content-center login-container">
                        <EditUser changePage={ChangePage} token={state.token} userID={state.userID}/>
                    </div>;
        case "UserProfile":
            return <UserProfile changePage={ChangePage} token={state.token} userID={state.userID}/>;
        
        case "Login":
        default:
            return <div className="container-flex d-flex align-items-center justify-content-center login-container">
                        <Login changePage={ChangePage} setTokenAndUserID={SetTokenAndUserID}/>
                    </div>;
    }
}

export default TripPlanner;
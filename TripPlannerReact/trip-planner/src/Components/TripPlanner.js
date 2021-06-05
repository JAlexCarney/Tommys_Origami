import React, {useState} from 'react';
import Login from './Login';
import CreateUser from './CreateUser';

let TripPlanner = () => {
    const [state, setState] = useState("LoginPage");
    const [token, setToken] = useState("");

    function ChangePage(page){
        setState(page);
    }
    function GetToken(newToken){
        setToken(newToken);
    }
    switch(state){
        case "LoginPage":
            return <div className="container d-flex align-items-center justify-content-center login-container">
                        <Login changePage={ChangePage} getToken={GetToken}/>
                    </div>;
        case "CreateUser":
            return <div className="container d-flex align-items-center justify-content-center login-container">
                        <CreateUser changePage={ChangePage} />
                    </div>;
    }
}

export default TripPlanner;
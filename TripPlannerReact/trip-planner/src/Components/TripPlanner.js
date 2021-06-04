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
        console.log(newToken);
        setToken(newToken);
    }
    switch(state){
        case "LoginPage":
            return <Login changePage={ChangePage} getToken={GetToken}/>;
        case "CreateUser":
            return <CreateUser />;
    }
}

export default TripPlanner;
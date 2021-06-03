import React, {useState} from 'react';
import Login from './Login';
import CreateUser from './CreateUser';

let TripPlanner = () => {
    const [state, setState] = useState("CreateUser");

    switch(state){
        case "loginPage":
            return <Login />;
        case "CreateUser":
            return <CreateUser />;
    }
}

export default TripPlanner;
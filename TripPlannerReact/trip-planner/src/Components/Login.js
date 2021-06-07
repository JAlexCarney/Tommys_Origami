import React, {useState} from 'react';
import ErrorMessage from './ErrorMessage';

let Login = (props) => {
    const [loginModel, setLoginModel] = useState({});
    const [error, setError] = useState("");

    let handleChange = (event) => {
        let newLoginModel = {...loginModel};
        newLoginModel[event.target.name] = event.target.value;
        setLoginModel(newLoginModel);
    }
    let onSubmit = (event) => {
        event.preventDefault();
        if(loginModel.identifier === undefined || loginModel.identifier === "")
        {
            setError("Username or Email is required");
            return;
        }
        else if(loginModel.password === undefined || loginModel.password === "")
        {
            setError("Password is required");
            return;
        }
        const init = {
            method: "POST",
            headers: {
                "Content-Type":"application/json",
                "Accept":"application/json"
            },
            body: JSON.stringify(loginModel)
        };
        fetch("https://localhost:44365/api/auth/login", init)
            .then(response => {
                if(response.status !== 200){
                    setError("Username or Password are incorrect");
                    return Promise.reject("Was not able to login.");
                }
                return response.json();
            })
            .then((json) => {
                props.setTokenAndUserID(json.token, json.userID);
            })
            .catch(console.log);
    }
    return (
        <div className="row">
            <div className="col">
            <h1 className="logo">TRIP PLANNER</h1>
            <form onSubmit={onSubmit} className="form-group" id="login">
                <div>
                    <label htmlFor="identifier" >Email/Username</label>
                    <input type="text" name="identifier" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="password" >Password</label>
                    <input type="password" name="password" onChange={handleChange}/>
                </div>
                <ErrorMessage message={error}/>
                <button className="btn btn-primary btn-submit" type="submit" >Login</button><br/>
                <button className="btn btn-secondary btn-create-account" type="button" onClick={() => {props.changePage("CreateUser")}}>Create Account</button>
            </form>
            </div>
        </div>
    )
}

export default Login;
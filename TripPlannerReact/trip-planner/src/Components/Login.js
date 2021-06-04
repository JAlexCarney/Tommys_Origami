import React, {useState} from 'react';

let Login = (props) => {
    const [loginModel, setLoginModel] = useState({});

    let handleChange = (event) => {
        let newLoginModel = {...loginModel};
        newLoginModel[event.target.name] = event.target.value;
        setLoginModel(newLoginModel);
    }
    let onSubmit = (event) => {
        event.preventDefault();
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
                    return Promise.reject("Was not able to login.")
                }
                return response.json();
            })
            .then(json => props.getToken(json.token))
            .catch(console.log);
    
    }
    return (
        <div>
            <form onSubmit={onSubmit} className="form-group" id="login">
                <div>
                    <label htmlFor="identifier" >Email/Username</label>
                    <input type="text" name="identifier" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="password" >Password</label>
                    <input type="password" name="password" onChange={handleChange}/>
                </div>
                <button type="submit" >Login</button>
                <button type="button" onClick={() => {props.changePage("CreateUser")}}>Create User</button>
            </form>
        </div>
    )
}

export default Login;
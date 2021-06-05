import React, { useState } from 'react';

let CreateUser = (props) => {
    const [user, setUser] = useState({});
    let handleChange = (event) => {
        let newUser = {...user};
        newUser[event.target.name] = event.target.value;
        setUser(newUser);
    }
    let onSubmit = (event) => {
        event.preventDefault();
        let today = new Date(); //year, month, day
        let dd = String(today.getDate()).padStart(2,"0");
        let mm = String(today.getMonth()+1).padStart(2,"0");
        let yyyy = String(today.getFullYear());
        today = yyyy + "-" + mm + "-" + dd;
        let newUser = {...user};
        console.log(newUser);
        newUser["dateCreated"] = today;
        delete newUser["confirmPassword"];
        setUser(newUser);
        const init = {
            method: "POST",
            headers: {
                "Content-Type":"application/json",
                "Accept":"application/json"
            },
            body: JSON.stringify(newUser)
        };
        fetch("https://localhost:44365/api/users", init)
            .then(response => {
                if(response.status !== 201){
                    return Promise.reject("Was not able to add user.")
                }
                return response.json();
            })
            .then()
            .catch(console.log);
    }
    return(
    <div className="row">
        <div className="col col-4"></div>
        <div className="col col-4">
            <form onSubmit={onSubmit} className="form-group">
                <div>
                    <label htmlFor="email">Email</label>
                    <input type="email" name="email" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="username">Username</label>
                    <input type="text" name="username" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input type="password" name="password" onChange={handleChange}/>
                </div>
                <div>
                    <label htmlFor="confirmPassword">Confirm Password</label>
                    <input type="password" name="confirmPassword" onChange={handleChange}/>
                </div>
                
                <button className="btn btn-primary btn-submit" type="submit">Submit</button><br/>
                <button className="btn btn-secondary btn-submit" type="button" onClick={() => {props.changePage("CreateUser")}}>Create User</button>
            </form>
        </div>
        <div className="col col-4"></div>
    </div>
    );
}

export default CreateUser;
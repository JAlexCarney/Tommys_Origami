import React, {useState} from 'react';

let Component = (props) => 
{
    const [state, setState] = useState({});

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    const handleCheck = (event) => {
        let newState = { ...state };
        if(event.target.value === "on")
        {
            newState[event.target.name] = true;
        }
        else
        {
            newState[event.target.name] = false;
        }
        setState(newState);
    }

    return (
        <div className="form">
            <h3 className="form-header">Adding Review</h3>
        <form onSubmit={(event) => {event.preventDefault(); console.log(state); props.handleAdd(state);}}>
            <div className="form-field">
                <label htmlFor="destination">Destination</label>
                <input type="text" name="destination" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="rating">Rating</label>
                <input type="radio" name="rating" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="description">Description</label>
                <input type="text" name="description" onChange={handleCheck}></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Add</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;
import React, {useState} from 'react';

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"></link>

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
                <div class="wrapper">
                    <input name="ratingRadio" type="radio" id="st1" value="1" />
                    <label for="st1"></label>
                    <input name="ratingRadio" type="radio" id="st2" value="2" />
                    <label for="st2"></label>
                    <input name="ratingRadio" type="radio" id="st3" value="3" />
                    <label for="st3"></label>
                    <input name="ratingRadio" type="radio" id="st4" value="4" />
                    <label for="st4"></label>
                    <input name="ratingRadio" type="radio" id="st5" value="5" />
                    <label for="st5"></label>
                </div>
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
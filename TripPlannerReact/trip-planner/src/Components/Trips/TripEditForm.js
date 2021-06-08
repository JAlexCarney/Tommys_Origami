import React, {useState, useEffect} from 'react';

let Component = (props) => 
{
    const [state, setState] = useState({});

    useEffect(() => {
        setState(props.trip);
    }, [props.trip]);

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
            <h3 className="form-header">Editing Trip{" " + props.agent.agentId}</h3>
        <form key={props.trip.tripID} onSubmit={(event) => {event.preventDefault(); props.handleEdit(state);}}>
            <div className="form-field">
                <label htmlFor="startDate">Start Date</label>
                <input type="date" defaultValue={props.agent.startDate} name="startDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="projectedEndDate">End Date</label>
                <input type="date" defaultValue={props.agent.endDate} name="projectedEndDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="isBooked">Booked?</label>
                <input type="checkbox" defaultValue={"off"} name="isBooked" onChange={handleCheck}></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;
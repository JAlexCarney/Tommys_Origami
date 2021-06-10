import React, {useState} from 'react';
import DestinationTrip from '../DestinationTrip/DestinationTrip';
import ErrorMessage from '../ErrorMessage';

let Component = (props) => 
{
    const [state, setState] = useState({});
    const [destinationTrips, setDestinationTrips] = useState([]);
    const [error, setError] = useState("");

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

    const addDestinations = (destinations) => {
        setDestinationTrips(destinations);
    }

    const submitAdd = (event) => {
        event.preventDefault();

        if(state.startDate === undefined){
            setError("start date is required");
            return;
        }
        if(state.projectedEndDate === undefined){
            setError("end date is required");
            return;
        }
        if(Date.parse(state.startDate) < Date.now()){
            setError("start date must be a future date");
            return;
        }
        if(Date.parse(state.projectedEndDate) < Date.parse(state.startDate)){
            setError("end date must be after start date");
            return;
        }

        props.handleAdd(state, destinationTrips);
    }

    return (
        <div className="form">
            <h3 className="form-header">Adding Trip</h3>
            <div className="form-field">
                <label className="label-small" htmlFor="startDate">Start Date:&ensp;</label>
                <input className="input-small" type="date" name="startDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label className="label-small" htmlFor="projectedEndDate">End Date:&ensp;</label>
                <input className="input-small" type="date" name="projectedEndDate" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label className="label-small">Booked:&ensp;</label>
                <input type="checkbox" name="isBooked" onChange={handleCheck}></input>
            </div>
            <DestinationTrip token={props.token} isAdd={true} addDestinations={addDestinations}/>
            <div className="text-center"><ErrorMessage message={error}/></div>
        <form onSubmit={submitAdd}>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Add</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;
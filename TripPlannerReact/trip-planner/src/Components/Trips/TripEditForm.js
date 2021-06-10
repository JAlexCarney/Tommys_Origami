import React, {useState, useEffect} from 'react';
import DestinationTrip from '../DestinationTrip/DestinationTrip';
import ErrorMessage from '../ErrorMessage';

let Component = (props) => 
{
    const [state, setState] = useState({});
    const [editedDestinations, setEditedDestinations] = useState([]);
    const [error, setError] = useState("");

    useEffect(() => {
        setState(props.trip);
    }, [props.trip]);

    if(state.tripID !== props.trip.tripID)
    {
        setState(props.trip);
    }

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    const handleCheck = (event) => {
        let newState = { ...state };
        newState[event.target.name] = !state.isBooked;
        setState(newState);
    }

    const editDestinations = (destinations) => {
        setEditedDestinations(destinations);
    }

    const submitEdit = (event) => {
        event.preventDefault();

        if(state.name === undefined || state.name === ""){
            setError("name is required");
            return;
        }
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
        props.handleEdit(state, editedDestinations);
    }

    return (
        <div className="form">
            <h3 className="form-header">Editing Trip{" " + props.trip.tripID}</h3>
            <div className="form-field" key={props.trip.name}>
                <label className="label-small" htmlFor="name">Name:&ensp;</label>
                <input className="input-small" type="text" defaultValue={props.trip.name} name="name" onChange={handleChange}></input>
            </div>
            <div className="form-field" key={props.trip.startDate.slice(0, 10)}>
                <label className="label-small" htmlFor="startDate">Start Date:&ensp;</label>
                <input className="input-small" type="date" defaultValue={props.trip.startDate.slice(0, 10)} name="startDate" onChange={handleChange}></input>
            </div>
            <div className="form-field" key={props.trip.projectedEndDate.slice(0, 10)}>
                <label className="label-small" htmlFor="projectedEndDate">End Date:&ensp;</label>
                <input className="input-small" type="date" defaultValue={props.trip.projectedEndDate.slice(0, 10)} name="projectedEndDate" onChange={handleChange}></input>
            </div>
            <div className="form-field" key={props.trip.isBooked}>
                <label className="label-small">Booked:&ensp;</label>
                <input type="checkbox" defaultChecked={props.trip.isBooked} name="isBooked" onChange={handleCheck}></input>
            </div>
            <DestinationTrip token={props.token} isAdd={false} tripID={props.trip.tripID} editDestinations={editDestinations}/>
            <div className="text-center"><ErrorMessage message={error}/></div>
        <form key={props.trip.tripID} onSubmit={submitEdit}>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;
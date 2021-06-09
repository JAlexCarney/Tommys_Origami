import React from 'react';

let Component = (props) => 
{
    let Destination = () => {
        if(props.review.Destination !== "" 
            && props.trip.actualEndDate !== null 
            && props.trip.actualEndDate !== undefined){
            return <td>{props.review.Destination.slice(0, Destination.name.length)}</td>;
        }
        return <td>-</td>;
    }

    
    return (
        <div className="form">
            <h3 className="form-header">Deleting Review{" " + props.review.reviewID}</h3>
            <table className="table table-striped">
                <tbody>
                    <tr>
                        <th>Destination</th>
                        <td>{props.review.Destination.slice(0, reveiw.Destination.length)}</td>
                    </tr>
                    <tr>
                        <th>Rating</th>
                        <td>{props.review.Rating.slice(0, 3)}</td>
                    </tr>
                </tbody>
            </table>
            <form onSubmit={(event => {event.preventDefault(); props.handleDelete(props.trip);})}>
                <button className="btn btn-danger btn-submit" type="submit">Confirm Delete</button><br/>
                <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
            </form>
        </div>
    );
}
export default Component;
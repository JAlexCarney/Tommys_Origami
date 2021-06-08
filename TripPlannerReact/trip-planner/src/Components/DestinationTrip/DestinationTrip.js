import React from 'react';

let DestinationsTable = (props) => 
{
    let handleAddDestination = (event) => {
        console.log(event.target);
    }
    let destinationOptions = (list) =>
    {
        return list.map((destination) => {
            return(
                <option value={destination.DestinationID}>{destination.city}</option>
            );
        });

    }
    return (
        <div>
            <table className="table table-striped" id="destinationTable">
                <thead className="thead-dark">
                    <tr>
                        <th></th>
                        <th>DESTINATIONS</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <div className="form-group">
                                <label htmlfor="Description" className="control-label">Description</label>
                                <input type="text" htmlfor="Description" className="form-control" />
                            </div>
                        </td>
                        <td>
                            <div className="form-group">
                                <label htmlfor="Destinations" className="control-label">Destinations</label>
                                <select className="Destinations">
                                    {destinationOptions(props.list)}
                                </select>
                            </div>
                        </td>
                        <td>
                            <span className="btn btn-primary" onClick={handleAddDestination}>&#43;</span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}
export default DestinationsTable;
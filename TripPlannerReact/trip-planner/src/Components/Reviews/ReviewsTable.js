import React from 'react';

let ReviewsTable = (props) => 
{
    let mapToTr = (list) =>
    {
        return list.map((review, i) => {
            return (
                <tr key={i} className="reviews-tr">
                    <td><button className="btn btn-secondary btn-round table-data" onClick={() => props.handleView(review)}>{review.destinationID}</button></td>
                    <td className="table-data">{review.description}</td>
                    <td className="table-data">{review.rating} ⭐</td>
                    <td><button className="btn btn-primary table-data btn-edit" onClick={() => props.handleUpdate(review)}>Edit</button>
                    <button className="btn btn-danger table-data btn-delete" onClick={() => props.handleDelete(review)}>Delete</button></td>
                </tr>
            );
        });
    }
    return (
        <div className="table-wrapper-scroll-y my-custom-scrollbar">
            <table className="table reviews-table">
                <thead className="thead reviews-thead">
                    <tr>
                        <th>Destination</th>
                        <th>Description</th>
                        <th>Rating</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody className="reviews-tbody">
                    <tr key={-1} className="reviews-tr">
                        <td className="table-data">-</td>
                        <td className="table-data">-</td>
                        <td className="table-data">-</td>
                        <td><button className="btn btn-primary table-data btn-add" onClick={() => props.handleAdd()}>Add Review</button></td>
                    </tr>
                    {mapToTr(props.list)}
                </tbody>
            </table>
        </div>
    );
}
export default ReviewsTable;
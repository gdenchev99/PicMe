import * as React from 'react'

export const InlineError = ({ field, errors }) => {
  if(!errors) {
    return null
  }

  if(!errors[field]) {
    return null
  }

  return (<div className='errors-container bg-danger text-light'>
    <ul>
      {errors[field].map(error => <li key={error[field] + error}>{error}</li>)}
    </ul>
  </div>)
}
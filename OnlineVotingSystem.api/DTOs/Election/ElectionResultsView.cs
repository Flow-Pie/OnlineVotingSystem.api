public record ElectionResultsView
(
     Guid CandidateId  ,
     Guid CandidateUserId,
     string CandidateName , 
     string Party  ,
     Guid ElectionPositionId  ,
     Guid PositionId ,
     string PositionName,
     Guid ElectionId ,
     string ElectionTitle,
     int TotalVotes
);
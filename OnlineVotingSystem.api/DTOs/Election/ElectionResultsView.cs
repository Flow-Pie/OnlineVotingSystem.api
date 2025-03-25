public record ElectionResultsView
(
     Guid ElectionId ,
     string ElectionTitle,
     Guid ElectionPositionId,
     Guid PositionId ,
     string PositionName,
     Guid CandidateId  ,
     Guid CandidateUserId,
     string CandidateName , 
     string Party  ,
     int TotalVotes,
     int RegisteredVoters,
     string VoterNames

);
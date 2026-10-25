namespace AI_Powered_Ticketing_and_Support_System.Feature.Categories.DTO;

public record CreateCategoryRequest(
    string Name,
    string Description,
    bool IsActive
);


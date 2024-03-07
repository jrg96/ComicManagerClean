using ComicManagerClean.Contracts.DTO.Character;

namespace ComicManagerClean.Contracts.Character;

public class GetCharacterListByFilterResponse
{
    public int TotalCount { get; set; }
    public IEnumerable<CharacterDto> Data { get; set; }
}

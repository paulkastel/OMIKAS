using System.Collections.Generic;

namespace OMIKAS
{
	public interface ISave
	{
		void saveFile(List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut);
		void sendEmail();
		void saveFileAndNotice(ProcessResults processResults, List<Alloy> alloys, List<Smelt> smelt, List<ProcessResults.Solution> solut);
	}
}
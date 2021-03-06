﻿namespace Cake.Prca.Issues.MsBuild
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="MsBuildCodeAnalysisProvider"/>.
    /// </summary>
    public class MsBuildCodeAnalysisSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildCodeAnalysisSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        protected MsBuildCodeAnalysisSettings(FilePath logFilePath, ILogFileFormat format, DirectoryPath repositoryRoot)
        {
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            this.Format = format;
            this.RepositoryRoot = repositoryRoot;

            using (var stream = new FileStream(logFilePath.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.LogFileContent = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildCodeAnalysisSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        protected MsBuildCodeAnalysisSettings(string logFileContent, ILogFileFormat format, DirectoryPath repositoryRoot)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            this.LogFileContent = logFileContent;
            this.Format = format;
            this.RepositoryRoot = repositoryRoot;
        }

        /// <summary>
        /// Gets the format of the MsBuild log file.
        /// </summary>
        public ILogFileFormat Format { get; private set; }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Gets the Root path of the repository.
        /// </summary>
        public DirectoryPath RepositoryRoot { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="MsBuildCodeAnalysisSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Instance of the <see cref="MsBuildCodeAnalysisSettings"/> class.</returns>
        public static MsBuildCodeAnalysisSettings FromFilePath(FilePath logFilePath, ILogFileFormat format, DirectoryPath repositoryRoot)
        {
            return new MsBuildCodeAnalysisSettings(logFilePath, format, repositoryRoot);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="MsBuildCodeAnalysisSettings"/> class from the content
        /// of a MsBuild log file.
        /// </summary>
        /// <param name="logFileContent">Content of the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Instance of the <see cref="MsBuildCodeAnalysisSettings"/> class.</returns>
        public static MsBuildCodeAnalysisSettings FromContent(string logFileContent, ILogFileFormat format, DirectoryPath repositoryRoot)
        {
            return new MsBuildCodeAnalysisSettings(logFileContent, format, repositoryRoot);
        }
    }
}
